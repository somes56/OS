//begin .js

$(function () {

    LoadMainList();

    $(document).keypress(function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            return false;
        }
    });

    $(document).keydown(function (e) {
        if (e.keyCode === 8 && !$(e.target).is("input, textarea")) {
            e.preventDefault();
            return false;
        }
    });

});

function DisableCheckBoxLotReceive() {

    var id = "input:checkbox[id*='ChkLotRcv_']";

    $(id).each(function () {
        if ($(this).is(":checked")) {
            $('#' + this.id).attr('disabled', true);
        }
    });
}

function LoadMainList() {

    var url = $('#RootURL').val() + "Main/LoadMainList";

    $('#MainListLoading').show();

    $.ajax({
        url: url,
        cache: false,
        error: function () {
            $('#MainListLoading').hide();
            $('#PartialMainList').text('Error in loading Main List');
        },
        success: function (data) {
            $('#MainListLoading').hide();
            $('#PartialMainList').html(data);
            DisableCheckBoxLotReceive();
        },
        type: "GET"
    });
}

function OSGo(OSNo) {

    var url = $('#RootURL').val() + "/Init/Init?OSNo=" + OSNo;

    window.location.href = url;

}

function LotReceive() {

    var id = "input:checkbox[id*='ChkLotRcv_']";
    var OSNos = '';

    $(id).each(function () {
        if ($(this).is(":checked") && $(this).is(":disabled") === false) {
            OSNos = OSNos + $('#' + this.id).val() + '|';
        }
    });

    UpdateLotMovementHist(OSNos);
}

function UpdateLotMovementHist(OSNos) {

    var OSNoArr = OSNos.split('|');

    for (var i = 0; i < OSNoArr.length - 1; i++) {
        var OSNo = '';
        OSNo = OSNoArr[i];

        var url = $('#RootURL').val() + "Main/UpdateLotMovementHist?OSNo=" + OSNo;

        $.ajax({
            url: url,
            error: function () {
                alert("Error in firing UpdateLotMovementHist");
            },
            success: function (data) {
                if (data === "" || data === "E") {
                    alert("Error in Updating Lot Movement History for " + OSNo);
                }
            },
            type: "POST"
        });
    }

    LoadMainList();
}