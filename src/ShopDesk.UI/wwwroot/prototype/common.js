const loaderSpinnerSelector = '#loader-spinner'

const Common = function () {
    this.bindEvents();
};

Common.prototype = {

    bindEvents: function () {

    },
    ajaxCallGetRequest: (url, callback = null) => {
        $.ajax({
            url: url,
            type: "get",
            dataType: 'json',
            beforeSend: function () {
                $(loaderSpinnerSelector).show();
            },
            success: function (response) {
                if(!response.status){
                    alert(response.message);
                    return;
                }

                if (callback)
                    callback(response.data);
            },
            error: (response) => {
                console.log(response);
                alert('Something went wrong, Please try again');
            },
            complete: function () {
                $(loaderSpinnerSelector).hide();
            }
        });
    },

    ajaxCallPostRequest: (url, data, callback = null) => {
        $.ajax({
            url: url,
            type: "post",
            data: data,
            dataType: 'json',
            headers: {
                'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
            },
            beforeSend: function () {
                $(loaderSpinnerSelector).show();
            },
            success: function (response) {
                if(!response.status){
                    alert(response.message);
                    return;
                }

                if (callback)
                    callback(response.data);
            },
            error: (response) => {
                console.log(response);
                alert('Something went wrong, Please try again');
            },
            complete: function () {
                 $(loaderSpinnerSelector).hide();
            }
        });
    },
    bindDropdown: (selector, values, valueProp, textProp, selectedValue, defaultLabel = '--Select--') => {
        $(selector).empty();
        if (defaultLabel !== '')
            $(selector).append(`<option value="">${defaultLabel}</option>`);

        values.map((item) => {
            $(selector).append(`<option ${selectedValue === item[valueProp] ? 'selected' : ''} value="${item[valueProp]}">${item[textProp]}</option>`);
        })
    }
}
