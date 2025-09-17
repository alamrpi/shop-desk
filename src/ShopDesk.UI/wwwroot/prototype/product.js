let Product = function () {
    this.bindEvents();
};

Product.prototype = {
    bindEvents: function () {
        this.handleStepNavigation();
        this.handleFormSubmission();
        this.handleSpecificationsSwitch();
        this.handleDynamicFields();
        this.handleSlugGeneration();
        this.handleCategoryCascade();
    },

    handleStepNavigation: function () {
        const self = this;
        $('#next-step-btn').on('click', function (e) {
            e.preventDefault();
            const productTypeId = $('#product-type-id').val();
            const userType = $('#user-id').val();

            if (productTypeId === "" || userType === "") {
                alert('Please select both product type and user id.');
                return;
            }

            $('#step1-container').hide();
            $('#step2-container').show();
            self.loadDynamicData(productTypeId);
        });

        $('#back-step-btn').on('click', function (e) {
            e.preventDefault();
            $('#step2-container').hide();
            $('#step1-container').show();
        });

        $('.product-type-select, #user-id').on('change', function () {
            self.checkStep1Selections();
        });

        this.checkStep1Selections();
    },

    checkStep1Selections: function() {
        const productTypeId = $('.product-type-select:checked').val();
        const userId = $('#user-id').val();

        if (productTypeId && userId) {
            $('#next-step-btn').prop('disabled', false);
            $('#product-type-id').val(productTypeId);
        } else {
            $('#next-step-btn').prop('disabled', true);
        }
    },

    handleFormSubmission: function () {
        const self = this;
        $('#product-form').on('submit', function (e) {
            if (!self.validateForm()) {
                e.preventDefault();
            }
        });
    },

    validateForm: function () {
        let isValid = true;

        // Clear all previous errors
        $('.invalid-feedback').remove();
        $('.is-invalid').removeClass('is-invalid');

        // Validate required fields
        $('#step2-container [required]').each(function() {
            if ($(this).val() === '') {
                isValid = false;
                $(this).addClass('is-invalid');
                $(this).after('<div class="invalid-feedback">This field is required.</div>');
            }
        });

        // Validate number fields
        $('#current_price, #previous_price, #total_in_stock').each(function() {
            const value = $(this).val();
            if (value !== '' && isNaN(value)) {
                isValid = false;
                $(this).addClass('is-invalid');
                $(this).after('<div class="invalid-feedback">Please enter a valid number.</div>');
            }
        });

        // Validate image file types
        $('#featured_image, #gallery_images').each(function() {
            if ($(this)[0].files.length > 0) {
                const files = $(this)[0].files;
                for (let i = 0; i < files.length; i++) {
                    const file = files[i];
                    if (!['image/jpeg', 'image/png', 'image/jpg', 'image/gif'].includes(file.type)) {
                        isValid = false;
                        $(this).addClass('is-invalid');
                        $(this).after('<div class="invalid-feedback">Invalid image file type. Only JPEG, PNG, JPG, GIF are allowed.</div>');
                        break;
                    }
                }
            }
        });

        return isValid;
    },
    loadProductCategories: function(productTypeId) {
        common.ajaxCallGetRequest('/api/product_categories/' + productTypeId, function(response) {
            common.bindDropdown('#product_category_id', response, 'id', 'name', '', '-- Select One --');
        });
    },

    loadDynamicData: function(productTypeId) {
        const self = this;

        common.ajaxCallGetRequest('/api/units/', function(response) {
            common.bindDropdown('#unit_id', response, 'id', 'name', '', '-- Select One --');
        });

        this.loadProductCategories(productTypeId);
    },
    handleCategoryCascade: function (){
        $('#product_category_id').on('change', function() {
            let categoryId = $(this).val();
            if (categoryId) {
                common.ajaxCallGetRequest('/api/product_sub_categories/' + categoryId, function(response) {
                    common.bindDropdown('#product_sub_category_id', response, 'id', 'name', '', '-- Select Sub Category --');
                });
            } else {
                $('#product_sub_category_id').empty().append('<option value="">--Select Sub Category--</option>');
            }
        });
    },

    handleSpecificationsSwitch: function () {
        $('#specifications-switch').on('change', function () {
            if ($(this).is(':checked')) {
                $('#specifications-fields-container').show();
            } else {
                $('#specifications-fields-container').hide();
            }
        });
    },

    handleDynamicFields: function () {
        let fieldIndex = $('.specification-field').length;
        const container = $('#specifications-fields-container');

        $(document).on('click', '#add-specification-btn', function () {
            const newField = `<div class="input-group mb-2 specification-field">
                                <input type="text" class="form-control" name="specifications[${fieldIndex}][name]" placeholder="Specification Name">
                                <input type="text" class="form-control" name="specifications[${fieldIndex}][description]" placeholder="Specification Description">
                                <button class="btn btn-danger remove-specification-btn" type="button"><i class="ph-light ph-minus"></i></button>
                              </div>`;
            container.append(newField);
            fieldIndex++;
        });

        $(document).on('click', '.remove-specification-btn', function () {
            $(this).closest('.specification-field').remove();
        });
    },

    handleSlugGeneration: function () {
        const self = this;
        $('#name').on('blur', function () {
            const name = $(this).val();
            if (name) {
                common.ajaxCallPostRequest('/api/generate_unique_slug', { name: name }, function (response) {
                    $('#slug').val(response.slug);
                });
            }
        });
    },
}
