/**
 * Provides APIs to get/set data in server side
 */
var ajaxCrudJS = function () {

    var that = this;

    /**
     * Insert update type
     */
    this.INSERT_UPDATE_TYPE = 0;

    /**
     * Modification update type
     */
    this.MODIFY_UPDATE_TYPE = 1;

    /**
     * Update type
     */
    this.updateType = this.MODIFY_UPDATE_TYPE;
    this.setUpdateType = function (inUpdateType) {
        this.updateType = inUpdateType;
    };

    /**
     * The Ajax URL in server side
     */
    this.ajaxUrl = "";
    this.setAjaxUrl = function (inAjaxUrl) {
        this.ajaxUrl = inAjaxUrl;
    };

    /**
     * The detail dialog
     */
    this.dlgModel = null;
    this.setDlgModel = function (inDlgModel) {
        this.dlgModel = inDlgModel;
    };

    /**
     * The source table
     */
    this.srcTable = null;
    this.setSrcTable = function (inSrcTable) {
        this.srcTable = inSrcTable;
    };

    /**
     * The update form
     */
    this.updateFrm = null;
    this.setUpdateFrm = function (inUpdateFrm) {
        this.updateFrm = inUpdateFrm;
    };

    /**
     * The validation rules
     */
    this.validationRules = null;
    this.setValidationRules = function (inValidationRules) {
        this.validationRules = inValidationRules;
    };

    /**
     * The validation messages
     */
    this.validationMsgs = null;
    this.setValidationMsgs = function (inValidationMsgs) {
        this.validationMsgs = inValidationMsgs;
    };

    /**
     * Set CK content to fix issue:
     * CKEditor submitting empty content to the form.
     */
    this.setCKContentFunc = null;
    this.setCKContentFunc = function(inSetCKContentFunc) {
        this.setCKContentFunc = inSetCKContentFunc;
    };

    /**
     * Build update request context accoring serialized form values
     */
    this.buildUpdateRequestContextFunc = null;
    this.setBuildUpdateRequestContextFunc = function (inBuildUpdateRequestContextFunc) {
        this.buildUpdateRequestContextFunc = inBuildUpdateRequestContextFunc;
    };

    /**
     * Set data for URL request
     */
    this.data = {};
    this.setData = function (inData) {
        this.data = inData;
    };

    /**
     * Ajax file upload setting
     */
    this.ajaxFileUploadSetting = null;
    this.setAjaxFileUploadSetting = function (inAjaxFileUploadSetting) {
        this.ajaxFileUploadSetting = inAjaxFileUploadSetting;
    };

    /**
     * The callback function
     */
    this.enterUrlCallbackFunc = null;
    this.setEnterUrlCallbackFunc = function (inCallbackFunc) {
        this.enterUrlCallbackFunc = inCallbackFunc;
    };

    /**
     * The callback function
     */
    this.getAddedRecordIdFunc = null;
    this.setAddedRecordIdFunc = function (inGetAddedRecordIdFunc) {
        this.getAddedRecordIdFunc = inGetAddedRecordIdFunc;
    };

    /**
     * Function to enter a view through Ajax request
     * Below data need to be set for using this function:
     * + ajaxUrl: the URL which is corresponding with item will be selected
     * + dlgModel: the jquery dialog instance will be shown as model
     */
    this.enterUrl = function () {
        var that = this;
        $.blockUI(); // Display loading image
        $.ajax({
            dataType: 'json',
            url: that.ajaxUrl,
            type: "GET",
            data: that.data
        }).done(function () {
        }).complete(function () {
        }).success(function (response) {
            if (response.Result) {
                that.enterUrlCallbackFunc(response.Data);
            } else {
                app.logger.error(response.message);
            }
            $.unblockUI(); // Hide loading image
        });
    };

    /**
     * Function to delete an record in server side using ajax
     * Below data need to be set for using this function:
     * + ajaxUrl: the URL which is corresponding with item will be selected
     * + recordId: the record identifier
     * + srcTable: the source table contains deleted record
     */
    this.deleteRecord = function () {
        var that = this;
        bootbox.confirm('Bạn có chắc chắn muốn xóa dữ liệu này?', function (confirmResult) {
            if (!confirmResult) {
                return;
            }
            // User has been confirmed to delete
            $.blockUI(); // Display loading image
            $.ajax({
                dataType: 'json',
                url: that.ajaxUrl,
                type: "GET",
                data: { id: that.data.id }
            }).done(function () {
            }).complete(function () {
            }).success(function (response) {
                if (response.Result) {
                    $.publish("RecordDeletedEvent", [that.data.id]);
                    app.logger.info(response.Description);
                } else {
                    app.logger.error(response.Description);
                }
                $.unblockUI(); // Hide loading image
            });
        });
    };

    /**
     * The function to insert/update item in server side using ajax
     * Below data need to be set for using this function:
     * + updateType: 0 - insert or 1 - update
     * + ajaxUrl: the URL which is corresponding with item will be updated
     * + validationRules: validation rules
     * + validationMsgs: validation messages
     * + srcTable: the source table contains updated record
     * + updateFrm: the update form
     * + dlgModel: the detail dialog
     * Using jquery validation plugin (jquery.validate.js) to validate
     */
    this.onUpdateRecord = function () {
        var that = this;

        // Remove validator has been assigned for this form before
        that.updateFrm.removeData("validator");

        // Assign validation for this form
        this.updateFrm.validate({
            errorElement: 'span',
            errorClass: 'help-block error',
            errorPlacement: function (error, element) {
                element.parents('.controls').append(error);
            },
            highlight: function (label) {
                $(label).closest('.control-group').removeClass('error success').addClass('error');
            },
            success: function (label) {
                label.addClass('valid').closest('.control-group').removeClass('error success').addClass('success');
            },
            rules: that.validationRules,
            messages: that.validationMsgs,
            submitHandler: function (form) {
                event.preventDefault();
                $.blockUI(); // Display loading image
                that.setCKContentFunc();
                var values = $(form).serializeObject();
                var requestContext = that.buildUpdateRequestContextFunc(values);
                $.ajax({
                    dataType: 'json',
                    url: that.ajaxUrl,
                    type: "post",
                    data: requestContext,
                    success: function (response) {
                        if (response.Result) {
                            // Function to refresh record table
                            var refreshRecordTableFunc = function () {
                                // Update data table after updating
                                switch (that.updateType) {
                                    case that.INSERT_UPDATE_TYPE:
                                        $.publish("RecordInsertedEvent", [response.Record]);
                                        break;

                                    case that.MODIFY_UPDATE_TYPE:
                                        $.publish("RecordUpdatedEvent", [response.Record]);
                                        break;
                                }
                                // Finished refreshing record table
                                app.logger.info(response.Description);
                                that.dlgModel.modal('hide'); // Hide detail dialog
                                $.unblockUI(); // Hide loading image
                            };

                            // Continue ajax upload if any file requested
                            if (that.ajaxFileUploadSetting != null) {
                                var defaultAjaxUploadSetting = {
                                    secureuri: false,
                                    dataType: 'json',
                                    success: function(uploadData, status) {
                                        refreshRecordTableFunc();
                                    },
                                    error: function(uploadData, status, e) {
                                        app.logger.error("Có lỗi trong quá trình upload file");
                                        that.dlgModel.modal('hide'); // Hide detail dialog
                                        $.unblockUI(); // Hide loading image
                                    }
                                };
                                // Setting for ajax uploading
                                var ajaxUploadSetting = $.extend(defaultAjaxUploadSetting, that.ajaxFileUploadSetting);
                                // Continue to upload image
                                if (that.updateType == that.INSERT_UPDATE_TYPE) {
                                    // The identifier of record has just been added
                                    var addedRecordId = that.getAddedRecordIdFunc(response.Record);
                                    ajaxUploadSetting.url += addedRecordId;
                                }
                                // Start to ajax upload file
                                $.ajaxFileUpload(ajaxUploadSetting);

                            } else { // Not upload file
                                refreshRecordTableFunc();
                            }
                        } else { // Failed to update record
                            app.logger.error(response.message);
                            that.dlgModel.modal('hide'); // Hide detail dialog
                            $.unblockUI(); // Hide loading image
                        }
                    }
                });
            }
        });
    };
};
