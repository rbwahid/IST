$('.single-date').daterangepicker({
    singleDatePicker: true,
    autoUpdateInput: false,
    showDropdowns: true,
    minYear: 2000,
    maxYear: parseInt(moment().format('YYYY'), 10),
    locale: {
        format: 'DD-MMM-YYYY'
    }
});

$('.single-date').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('DD-MMM-YYYY'));
});

$('.single-date').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
});

$(document).on("click", ".single-date-icon", function () {

    $(this).parent().find('.single-date').focus();
});
//$(document).on("click", ".myTimepickerIcon", function () {

//    $(this).parent().find('.timepicker').focus();
//});