$(function () {

    var documentUploadInProgress = false;

    $("#tabs, .tabs").tabs();

    $(".chosen").chosen();

    if ($("#categories").length) {

        var nested = $(".categories-list ul").eq(0);
        nested.nestedSortable({
            listType: 'ul',
            disableNesting: 'no-nest',
            forcePlaceholderSize: true,
            handle: 'div',
            helper: 'clone',
            items: 'li',
            opacity: .6,
            placeholder: 'placeholder',
            revert: 250,
            tabSize: 25,
            tolerance: 'pointer',
            toleranceElement: '> div'
        });

        $("#save-ordering").click(function () {
            document.location.href = "/Admin/Category/SaveOrdering?" + nested.nestedSortable('serialize');
        });
    } else {

        $(".products-list ul").eq(0).sortable();
        
        $("#save-ordering").click(function () {
            var cid = $("#cid").val();
            document.location.href = "/Admin/Product/Reorder?categoryId=" + cid + "&" + $(".products-list ul").eq(0).sortable('serialize');
        });
    }

    $("#date").datepicker();

    window.setInterval(function () {
        $("#icon").each(function () {
            var el = $(this);

            if (!documentUploadInProgress && el.val().length > 0) {
                el.parents("form").submit();
                documentUploadInProgress = true;
            }

        });
    }, 500);

    $("[id=ajaxUploadFormImg]").ajaxForm({
        iframe: true,
        dataType: "json",
        beforeSubmit: function () {
            $("[id=ajaxUploadFormImg]").hide();
            $("[id=ajaxUploadFormImg]").after('<span class="uploading">Nahrávám...</span>');
        },
        success: function (result) {

            $("[id=ajaxUploadFormImg]").resetForm();
            $("[id=ajaxUploadFormImg]").show();
            $(".uploading").remove();

            if (result.id) {
                $('<img />').attr("src", '/Image/Thumbnail?path=' + result.path + "&width=200&height=200").load(function () {
                    $(".uploaded.img").html($(this));
                    $("#imageId").val(result.id);
                });
            }
            documentUploadInProgress = false;
        },
        error: function (xhr, textStatus, errorThrown) {
            $("[id=ajaxUploadFormImg]").resetForm();
            documentUploadInProgress = false;
        }
    }); /**/

});

jQuery(function ($) {

    if (!$.datepicker) return;

    $.datepicker.regional['cs'] = {
        closeText: 'Zavřít',
        prevText: '&#x3c;Dříve',
        nextText: 'Později&#x3e;',
        currentText: 'Nyní',
        monthNames: ['leden', 'únor', 'březen', 'duben', 'květen', 'červen',
        'červenec', 'srpen', 'září', 'říjen', 'listopad', 'prosinec'],
        monthNamesShort: ['led', 'úno', 'bře', 'dub', 'kvě', 'čer',
        'čvc', 'srp', 'zář', 'říj', 'lis', 'pro'],
        dayNames: ['neděle', 'pondělí', 'úterý', 'středa', 'čtvrtek', 'pátek', 'sobota'],
        dayNamesShort: ['ne', 'po', 'út', 'st', 'čt', 'pá', 'so'],
        dayNamesMin: ['ne', 'po', 'út', 'st', 'čt', 'pá', 'so'],
        weekHeader: 'Týd',
        dateFormat: 'dd.mm.yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };

    $.datepicker.setDefaults($.datepicker.regional['cs']);

    $(".delete").click(function () {
        return confirm("Opravdu chcete tuto položku smazat?");
    });

});