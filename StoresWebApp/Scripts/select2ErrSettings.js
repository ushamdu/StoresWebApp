function showSelect2Error($select, message) {
    var $column = $select.closest('.col-md-6');    // The whole column div
    var $container = $select.next('.select2');    // Select2 wrapper

    // Remove old errors
    $column.find('.select2-error').remove();

    // Add border highlight to Select2
    $container.find('.select2-selection').css({
        borderColor: '#a94442'
    });

    // Insert error BELOW the entire column
    //$column.after('<div class="select2-error" style="color:#a94442;margin-top:5px;">' + message + '</div>');
}

function clearSelect2Error($select) {
    var $column = $select.closest('.col-md-6');
    var $container = $select.next('.select2');

    $column.next('.select2-error').remove();
    $container.find('.select2-selection').css({
        borderColor: ''
    });
}
