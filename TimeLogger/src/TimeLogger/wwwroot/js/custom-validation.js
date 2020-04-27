$.validator.addMethod('DurationMinutes',
    function (value, element, params) {
       
        return false;
    });

$.validator.unobtrusive.adapters.addBool('DurationMinutes');