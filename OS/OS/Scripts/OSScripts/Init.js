//begin .js

$(function () {

    $("#ClientVerifyBlock").hide();

    $('#RbSBNA').attr('disabled', true);
    $('#RbSBOK').attr('disabled', true);
    $('#RbSBNG').attr('disabled', true);
    $('#RbCANA').attr('disabled', true);
    $('#RbCAOK').attr('disabled', true);
    $('#RbCANG').attr('disabled', true);
    $('#RbXANA').attr('disabled', true);
    $('#RbXAOK').attr('disabled', true);
    $('#RbXANG').attr('disabled', true);
    $('#RbAANA').attr('disabled', true);
    $('#RbAAOK').attr('disabled', true);
    $('#RbAANG').attr('disabled', true);
    $('#RbSANA').attr('disabled', true);
    $('#RbSAOK').attr('disabled', true);
    $('#RbSANG').attr('disabled', true);
    $('#RbOANA').attr('disabled', true);
    $('#RbOAOK').attr('disabled', true);
    $('#RbOANG').attr('disabled', true);

    $(document).keypress(function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            return false;
        }
    });

    $(document).keydown(function (e) {
        if (e.keyCode === 8 && !$(e.target).is("input, textarea")){
            e.preventDefault();
            return false;
        }
    });

    $('#SearchString').keyup(function () {
        this.value = this.value.toUpperCase();
    });

    ToggleBinJudgement();
    ToggleManualJudgement();
    ToggleXrayJudgement();
    ToggleApperanceJudgment();
    ToggleSemJudgment();

    LoadUploadFile('XA');
    LoadUploadFile('AA');
    LoadUploadFile('SA');

});

$('#OpenQty').keyup(function () {

    var OpenQty = 0;
    var ShortQty = 0;
    var OpenShortQty = 0;

    OpenQty = $('#OpenQty').val();
    ShortQty = $('#ShortQty').val();

    OpenShortQty = parseInt(OpenQty || 0) + parseInt(ShortQty || 0);

    $('#TtlAlyQty').val(OpenShortQty);
    $('#TtlOSQty').val(OpenShortQty);
});

$('#ShortQty').keyup(function () {
    
    var OpenQty = 0;
    var ShortQty = 0;
    var OpenShortQty = 0;

    OpenQty = $('#OpenQty').val();
    ShortQty = $('#ShortQty').val();

    OpenShortQty = parseInt(OpenQty || 0) + parseInt(ShortQty || 0);

    $('#TtlAlyQty').val(OpenShortQty);
    $('#TtlOSQty').val(OpenShortQty);
});

function Numbers(evt) {

    var e = event || evt;
    var charCode = e.which || e.keyCode;

    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function ToggleBinJudgement() {

    var BinJudge = $('#BinJudge').val();

    if (BinJudge === "OK") {
        $('#lblBinNG').hide();
        $('#lblLotHold').hide();
        $('#ChkSB').attr('checked', true);
        $('#ChkSB').attr('disabled', true);
    } else if (BinJudge === "NG") {
        $('#lblBinOK').hide();
        $('#lblLotFlow').hide();
        $('#ChkSB').attr('checked', true);
        $('#ChkSB').attr('disabled', true);
    } else {
        $('#lblBinNG').show();
        $('#lblBinOK').show();
        $('#lblLotFlow').show();
        $('#lblLotHold').show();
        $('#ChkSB').attr('checked', false);
        $('#ChkSB').attr('disabled', false);
    }
}

function ToggleManualJudgement() {

    var CtmJudge = $('#CtmJudge').val();

    if (CtmJudge === "OK") {
        $('#lblCANG').hide();
        $('#lblCACmpltHist').show();
        $('#ChkCA').attr('checked', true);
        $('#ChkCA').attr('disabled', true);
    } else if (CtmJudge === "NG") {
        $('#lblCAOK').hide();
        $('#lblCACmpltHist').show();
        $('#ChkCA').attr('checked', true);
        $('#ChkCA').attr('disabled', true);
    } else {
        $('#lblCANG').show();
        $('#lblCAOK').show();
        $('#lblCACmpltHist').hide();
        $('#ChkCA').attr('checked', false);
        $('#ChkCA').attr('disabled', false);
    }
}

function ToggleXrayJudgement() {

    var XrayJudge = $('#XrayJudge').val();

    if (XrayJudge === "OK") {
        $('#lblXANG').hide();
        $('#lblXACmpltHist').show();
        $('#ChkXA').attr('checked', true);
        $('#ChkXA').attr('disabled', true);
    } else if (XrayJudge === "NG") {
        $('#lblXAOK').hide();
        $('#lblXACmpltHist').show();
        $('#ChkXA').attr('checked', true);
        $('#ChkXA').attr('disabled', true);
    } else {
        $('#lblXANG').show();
        $('#lblXAOK').show();
        $('#lblXACmpltHist').hide();
        $('#ChkXA').attr('checked', false);
        $('#ChkXA').attr('disabled', false);
    }
}

function ToggleApperanceJudgment() {

    var AppJudge = $('#AppJudge').val();

    if (AppJudge === "OK") {
        $('#lblAANG').hide();
        $('#lblAACmpltHist').show();
        $('#ChkAA').attr('checked', true);
        $('#ChkAA').attr('disabled', true);
    } else if (AppJudge === "NG") {
        $('#lblAAOK').hide();
        $('#lblAACmpltHist').show();
        $('#ChkAA').attr('checked', true);
        $('#ChkAA').attr('disabled', true);
    } else {
        $('#lblAANG').show();
        $('#lblAAOK').show();
        $('#lblAACmpltHist').hide();
        $('#ChkAA').attr('checked', false);
        $('#ChkAA').attr('disabled', false);
    }
}

function ToggleSemJudgment() {

    var SemJudge = $('#SemJudge').val();

    if (SemJudge === "OK") {
        $('#lblSANG').hide();
        $('#lblSACmpltHist').show();
        $('#ChkSA').attr('checked', true);
        $('#ChkSA').attr('disabled', true);
    } else if (SemJudge === "NG") {
        $('#lblSAOK').hide();
        $('#lblSACmpltHist').show();
        $('#ChkSA').attr('checked', true);
        $('#ChkSA').attr('disabled', true);
    } else {
        $('#lblSANG').show();
        $('#lblSAOK').show();
        $('#lblSACmpltHist').hide();
        $('#ChkSA').attr('checked', false);
        $('#ChkSA').attr('disabled', false);
    }
}

function CAComplete() {

    var OSNo = $('#OSNo').val();
    var StatusCode = $('#StatusCode').val();
    var CAStatus = false;
    var CtmJudge = "NA";
    var OverAllJudge = "NA";
    var Result = "";

    if (OSNo !== "") {
		
        if (StatusCode === "CA_EXEC_REQ") {
			
            if ($('#ChkCA').is(':checked')) {
                
				Result = GetCAResult();
				
                if (Result !== "E" && Result !== "") {

                    if (Result === "NG") {
                        CAStatus = true;
                        CtmJudge = "NG";
                    } else {
                        CAStatus = true;
                        CtmJudge = "OK";
                        OverAllJudge = "OK";
                    }

                } else {
                    alert("Error in getting Manual Analysis result");
                }
				
            }
			
            $('#CAStatus').val(CAStatus);
            $('#CtmJudge').val(CtmJudge);
            $('#OverAllJudge').val(OverAllJudge);
            return true;
			
        } else {
            return false;
        }
		
    } else {
        alert("OSNo is required");
        return false;
    }
}

function XAComplete() {

    var OSNo = $('#OSNo').val();
    var StatusCode = $('#StatusCode').val();
    var Result = "";
    var XAStatus = false;
    var XrayJudge = "NA";
    var OverAllJudge = "NA";
    var Msg = [];
    var i = 0;
    

    if (OSNo !== "") {

        if (StatusCode === "XA_EXEC_REQ") {

            if ($('#XADefectID1').val() !== "" || $('#XADefectID2').val() !== "" || $('#XADefectID3').val() !== ""
                || $('#XADefectID4').val() !== "" || $('#XADefectID5').val() !== "" || $('#XADefectID6').val() !== "") {

                if ($('#XADefectID1').val() !== "" && $('#XADefectQty1').val() === "0") {
                    Msg[i] = "X-Ray Defect 1 Quantity is required";
                    i = i + 1;
                }

                if ($('#XADefectID2').val() !== "" && $('#XADefectQty2').val() === "0") {
                    Msg[i] = "X-Ray Defect 2 Quantity is required";
                    i = i + 1;
                }

                if ($('#XADefectID3').val() !== "" && $('#XADefectQty3').val() === "0") {
                    Msg[i] = "X-Ray Defect 3 Quantity is required";
                    i = i + 1;
                }

                if ($('#XADefectID4').val() !== "" && $('#XADefectQty4').val() === "0") {
                    Msg[i] = "X-Ray Defect 4 Quantity is required";
                    i = i + 1;
                }

                if ($('#XADefectID5').val() !== "" && $('#XADefectQty5').val() === "0") {
                    Msg[i] = "X-Ray Defect 5 Quantity is required";
                    i = i + 1;
                }

                if ($('#XADefectID6').val() !== "" && $('#XADefectQty6').val() === "0") {
                    Msg[i] = "X-Ray Defect 6 Quantity is required";
                    i = i + 1;
                }

                if (Msg.length === 0) {

                    if ($('#ChkXA').is(':checked')) {

                        Result = GetXAResult();

                        if (Result !== "E" && Result !== "") {

                            if (Result === "XA-NG") {
                                XAStatus = true;
                                XrayJudge = "NG";
                            } else if (Result === "XA-OK") {
                                XAStatus = true;
                                XrayJudge = "OK";
                            } else if (Result === "OA-NG") {
                                XAStatus = true;
                                XrayJudge = "NG";
                                OverAllJudge = "NG";
                            } else {
                                XAStatus = true;
                                XrayJudge = "OK";
                                OverAllJudge = "OK";
                            }

                        } else {
                            alert("Error in getting X-Ray Analysis result");
                        }

                    }

                    $('#XAStatus').val(XAStatus);
                    $('#XrayJudge').val(XrayJudge);
                    $('#OverAllJudge').val(OverAllJudge);
                    return true;

                } else {
                    alert(Msg.join("\n"));
                    return false;
                }

            } else {
                alert("X-Ray defect is required");
                return false;
            }

        } else {
            return false;
        }

    } else {
        alert("OSNo is required");
        return false;
    }
}

function AAComplete() {

    var OSNo = $('#OSNo').val();
    var StatusCode = $('#StatusCode').val();
    var Result = "";
    var AAStatus = false;
    var AppJudge = "NA";
    var OverAllJudge = "NA";
    var Msg = [];
    var i = 0;


    if (OSNo !== "") {

        if (StatusCode === "AA_EXEC_REQ") {

            if ($('#AADefectID1').val() !== "" || $('#AADefectID2').val() !== "" || $('#AADefectID3').val() !== ""
                || $('#AADefectID4').val() !== "" || $('#AADefectID5').val() !== "" || $('#AADefectID6').val() !== "") {

                if ($('#AADefectID1').val() !== "" && $('#AADefectQty1').val() === "0") {
                    Msg[i] = "Appearance Defect 1 Quantity is required";
                    i = i + 1;
                }

                if ($('#AADefectID2').val() !== "" && $('#AADefectQty2').val() === "0") {
                    Msg[i] = "Appearance Defect 2 Quantity is required";
                    i = i + 1;
                }

                if ($('#AADefectID3').val() !== "" && $('#AADefectQty3').val() === "0") {
                    Msg[i] = "Appearance Defect 3 Quantity is required";
                    i = i + 1;
                }

                if ($('#AADefectID4').val() !== "" && $('#AADefectQty4').val() === "0") {
                    Msg[i] = "Appearance Defect 4 Quantity is required";
                    i = i + 1;
                }

                if ($('#AADefectID5').val() !== "" && $('#AADefectQty5').val() === "0") {
                    Msg[i] = "Appearance Defect 5 Quantity is required";
                    i = i + 1;
                }

                if ($('#AADefectID6').val() !== "" && $('#AADefectQty6').val() === "0") {
                    Msg[i] = "Appearance Defect 6 Quantity is required";
                    i = i + 1;
                }

                if (Msg.length === 0) {

                    if ($('#ChkAA').is(':checked')) {

                        Result = GetAAResult();

                        if (Result !== "E" && Result !== "") {

                            if (Result === "AA-NG") {
                                AAStatus = true;
                                AppJudge = "NG";
                            } else if (Result === "AA-OK") {
                                AAStatus = true;
                                AppJudge = "OK";
                            } else if (Result === "OA-NG") {
                                AAStatus = true;
                                AppJudge = "NG";
                                OverAllJudge = "NG";
                            } else {
                                AAStatus = true;
                                AppJudge = "OK";
                                OverAllJudge = "OK";
                            }

                        } else {
                            alert("Error in getting Appearance Analysis result");
                        }

                    }

                    $('#AAStatus').val(AAStatus);
                    $('#AppJudge').val(AppJudge);
                    $('#OverAllJudge').val(OverAllJudge);
                    return true;

                } else {
                    alert(Msg.join("\n"));
                    return false;
                }

            } else {
                alert("Appearance defect is required");
                return false;
            }

        } else {
            return false;
        }

    } else {
        alert("OSNo is required");
        return false;
    }
}

$('#XAFileUpload').on('change', function (e) {

    var OSNo = $('#OSNo').val();

    UploadFile(OSNo, 'XA', e);
});

$('#AAFileUpload').on('change', function (e) {

    var OSNo = $('#OSNo').val();

    UploadFile(OSNo, 'AA', e);
});

$('#SAFileUpload').on('change', function (e) {

    var OSNo = $('#OSNo').val();

    UploadFile(OSNo, 'SA', e);
});

$('#OSNo').on('change', function () {

    var OSNo = $('#OSNo').val();
    var url = $('#RootURL').val() + 'Init/Init?OSNo=' + OSNo;

    window.location.href = url;
});

function ResetForm() {

    var url = $('#RootURL').val() + "Init/Init";
    window.location.href = url;
}

function GetCAResult() {

    var result = "";
    var url = $('#RootURL').val() + "Init/GetCAResult?OSNo=" + $('#OSNo').val() + "&TtlAlyQty=" + $('#TtlAlyQty').val();

    $.ajax({
        url: url,
        async: false,
        error: function () {
            alert("Error in calling GetCAResult function");
            result = "E";
        },
        success: function (data) {
            result = data;
        },
        type: "GET"
    });
    
    return result;
}

function GetXAResult() {

    var result = "";
    var url = $('#RootURL').val() + "Init/GetXAResult";

    $.ajax({
        url: url,
        async: false,
        data: {
            OSNo: $('#OSNo').val(),
            DefectID1: $('#XADefectID1').val(),
            DefectID2: $('#XADefectID2').val(),
            DefectID3: $('#XADefectID3').val(),
            DefectID4: $('#XADefectID4').val(),
            DefectID5: $('#XADefectID5').val(),
            DefectID6: $('#XADefectID6').val(),
            DefectQty1: $('#XADefectQty1').val(),
            DefectQty2: $('#XADefectQty2').val(),
            DefectQty3: $('#XADefectQty3').val(),
            DefectQty4: $('#XADefectQty4').val(),
            DefectQty5: $('#XADefectQty5').val(),
            DefectQty6: $('#XADefectQty6').val()
        },
        error: function () {
            alert("Error in calling GetXAResult function");
            result = "E";
        },
        success: function (data) {
            result = data;
        },
        type: "GET"
    });
    return result;
}

function GetAAResult() {

    var result = "";
    var url = $('#RootURL').val() + "Init/GetAAResult";

    $.ajax({
        url: url,
        async: false,
        data: {
            OSNo: $('#OSNo').val(),
            DefectID1: $('#AADefectID1').val(),
            DefectID2: $('#AADefectID2').val(),
            DefectID3: $('#AADefectID3').val(),
            DefectID4: $('#AADefectID4').val(),
            DefectID5: $('#AADefectID5').val(),
            DefectID6: $('#AADefectID6').val(),
            DefectQty1: $('#AADefectQty1').val(),
            DefectQty2: $('#AADefectQty2').val(),
            DefectQty3: $('#AADefectQty3').val(),
            DefectQty4: $('#AADefectQty4').val(),
            DefectQty5: $('#AADefectQty5').val(),
            DefectQty6: $('#AADefectQty6').val()
        },
        error: function () {
            alert("Error in calling GetXAResult function");
            result = "E";
        },
        success: function (data) {
            result = data;
        },
        type: "GET"
    });
    return result;
}

function LoadUploadFile(OSScope) {

    var OSNo = $('#OSNo').val();
    var url = $('#RootURL').val() + "Cmn/LoadUploadFile?OSNo=" + OSNo + "&OSScope=" + OSScope;
    var id = "#Partial" + OSScope + "RefFile";

    $.ajax({
        url: url,
        cache: false,
        error: function () {
            $(id).text('Error in calling LoadUploadFile function');
        },
        success: function (data) {
            $(id).html(data);
        },
        type: "GET"
    });
}

function UploadFile(OSNo, OSScope, e) {

    var url = $('#RootURL').val() + "Cmn/UploadFile?OSNo=" + OSNo + "&OSScope=" + OSScope;

    var files = e.target.files;

    if (OSNo !== "") {

        if (files.length > 0) {

            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
            }

            $.ajax({
                url: url,
                contentType: false,
                processData: false,
                data: data,
                error: function () {
                    alert("Error in uploading file");
                },
                type: "POST"
            });

            LoadUploadFile(OSScope);
        }

    }
}

function UpdateRefFileComment(OSNo, OSScope, FileName, Comment) {

    var url = $('#RootURL').val() + "Cmn/UpdateRefFileComment?OSNo=" + OSNo + "&OSScope=" + OSScope + "&FileName=" + FileName + "&Comment=" + Comment.value;

    $.ajax({
        url: url,
        error: function () {
            alert("Error in updating comment");
        },
        type: "POST"
    });

    LoadUploadFile(OSScope);
}

function DeleteUploadFile(OSNo, OSScope, FileName) {

    var url = $('#RootURL').val() + "Cmn/DeleteUploadFile?OSNo=" + OSNo + "&OSScope=" + OSScope + "&FileName=" + FileName;

    $.ajax({
        url: url,
        error: function () {
            alert("Error in deleting file");
        },
        type: "POST"
    });

    LoadUploadFile(OSScope);
}

function SearchOS() {

    var crit = {
        "mt": "Search OS",
        "fn": "OS",
        "p": {},
        "md": {
            "md1": "OSNo"
        }
    };
    OpenAdvSearch(JSON.stringify(crit));
}

function SearchDefect(OSScope, Md1, Md2) {

    var crit = {
        "mt": "Search Defect",
        "fn": "Defect",
        "p": {
            "p1":OSScope
        },
        "md": {
            "md1": Md1,
            "md2":Md2
        }
    };
    OpenAdvSearch(JSON.stringify(crit));
}

function SubmitValidation() {

    var msg = [];
    var i = 0;

    if ($('#OSNo').val() === "") {
        msg[i] = "OS No is required.";
        i = i + 1;
    }

    if ($('#BaseProductName').val() === "") {
        msg[i] = "Base Product Name is required.";
        i = i + 1;
    }

    if ($('#PkgDesc').val() === "") {
        msg[i] = "Package is required.";
        i = i + 1;
    }

    if ($('#LotQty').val() === "0") {
        msg[i] = "Lot Quantity is required.";
        i = i + 1;
    }

    if (msg.length > 0) {
        $("#ClientVerifyMsg").html(msg.join("<br/>"));

        $("#ClientVerifyBlock").show();

        return false;
    } else {
        return true;
    }
}

