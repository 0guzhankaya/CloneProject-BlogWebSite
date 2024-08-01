$(document).ready(function () {

    // Trumbowyg started.
    $('#text-editor').trumbowyg({
        btns: [
            ['viewHTML'],
            ['undo', 'redo'], // Only supported in Blink browsers
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['insertImage'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['foreColor', 'backColor'],
            ['fontfamily'],
            ['fontsize'],
            ['emoji'],
            ['fullscreen']
        ],
        plugins: {
            colors: {
                foreColorList: [
                    'ff0000', '00ff00', '0000ff', '4F1787'
                ],
                backColorList: [
                    '000', '333', '555', 'F6FB7A'
                ],
                displayAsList: false
            }
        }
    });
    // Trumbowyg ended.

    // Select2 started.
    $('#categoryList').select2({
        theme: 'bootstrap4',
        placeholder: "Lütfen bir kategori seçiniz...",
        allowClear: true,
    });

    // Select2 ended.

    // jQuery UI - Datepicker started.
    $(function () {
        $("#datepicker").datepicker({
            closeText: "kapat",
            prevText: "geri",
            nextText: "ileri",
            currentText: "bugün",
            monthNames: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
                "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
            monthNamesShort: ["Oca", "Şub", "Mar", "Nis", "May", "Haz",
                "Tem", "Ağu", "Eyl", "Eki", "Kas", "Ara"],
            dayNames: ["Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi"],
            dayNamesShort: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
            dayNamesMin: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
            weekHeader: "Hf",
            dateFormat: "dd.mm.yy",
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: "",
            duration: 400,
            showAnim: "fold",
            showOptions: {direction:"up"},
            minDate: -3,
            maxDate: +3,
        });
    });

    // jQuery UI - Datepicker ended.
});