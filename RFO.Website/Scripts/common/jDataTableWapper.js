/**
 * Provides APIs to get/set data in server side
 */
var jDataTableWapperJS = function () {
    var that = this;

    /**
     * Maximum number of record will be retrieved
     */
    this.MAX_NUM_RETRIEVED_RECORD = 100000;

    /**
     * The URL for loading records from server side
     */
    this.selectUrl = "";
    this.setSelectUrl = function (inSelectUrl) {
        this.selectUrl = inSelectUrl;
    };

    /**
     * The javascript function for displaying the record detail
     */
    this.addNewFunctionJS = "";
    this.setAddNewFunctionJS = function (inAddNewFunctionJS) {
        this.addNewFunctionJS = inAddNewFunctionJS;
    };

    /**
     * The javascript function for entering the advance search
     */
    this.enterAdvanceSearchFunctionJS = "";
    this.setEnterAdvanceSearchFunctionJS = function (inAdvanceSearchFunctionJS) {
        this.enterAdvanceSearchFunctionJS = inAdvanceSearchFunctionJS;
    };

    /**
     * The URL for loading records from server side
     */
    this.localizedMessages = null;
    this.setLocalizedMessages = function (inLocalizedMessages) {
        this.localizedMessages = inLocalizedMessages;
    };

    this.aLengthMenu = [[20, 50, 100], [20, 50, 100]];
    this.setALengthMenu = function (inALengthMenu) {
        this.aLengthMenu = inALengthMenu;
    };

    /**
     * Default display length
     */
    this.defaultDisplayLength = 20;
    this.setDefaultDisplayLength = function (inDefaultDisplayLength) {
        this.defaultDisplayLength = inDefaultDisplayLength;
    };

    /**
     * The custom buttons area identifier
     */
    this.customButtonsAreaId = "";

    /**
     * The custom buttons area class
     */
    this.customButtonsAreaClass = "";

    /**
     * The custom buttons area
     */
    this.getCustomButtonsArea = function() {
        return this.customButtonsAreaId + '.' + this.customButtonsAreaClass;
    };

    /**
     * The record identifier
     */
    this.srcTable = null;
    this.setSrcTable = function (inSrcTable) {
        this.srcTable = inSrcTable;
        var tblId = this.srcTable.attr("id");
        this.customButtonsAreaId = '#' + tblId + '-customButtonsArea';
        this.customButtonsAreaClass = 'customButtonsArea';
    };

    /**
     * The aocolumns
     */
    this.aoColumns = null;
    this.setAoColumns = function (inAoColumns) {
        this.aoColumns = inAoColumns;
    };

    /**
     * The aoColumnDefs
     */
    this.aoColumnDefs = null;
    this.setAoColumnDefs = function (inAoColumnDefs) {
        this.aoColumnDefs = inAoColumnDefs;
    };

    /**
     * The aaSorting
     */
    this.aaSorting = [[0, "asc"]];
    this.setAaSorting = function (inAaSorting) {
        this.aaSorting = inAaSorting;
    };

    /**
     * The callback function is called when a TR element is created
     */
    this.fnCreatedRow = null;
    this.setFnCreatedRow = function (inFnCreatedRow) {
        this.fnCreatedRow = inFnCreatedRow;
    };

    /**
     * The callback function is called when finishing to draw table
     */
    this.fnDrawCallback = null;
    this.setFnDrawCallback = function (inFnDrawCallback) {
        this.fnDrawCallback = inFnDrawCallback;
    };

    /**
     * Determine whether or not need to display add new button
     */
    this.needDisplayAddNewBtn = false;
    this.setNeedDisplayAddNewBtn = function (inNeedDisplayAddNewBtn) {
        this.needDisplayAddNewBtn = inNeedDisplayAddNewBtn;
    };

    /**
     * Determine whether or not need to display visible setting column
     */
    this.needDisplayColVis = false;
    this.setNeedDisplayColVis = function (inNeedDisplayColVis) {
        this.needDisplayColVis = inNeedDisplayColVis;
    };

    /**
     * Determine whether or not need to display advance search button
     */
    this.needAdvanceSearchBtn = false;
    this.setNeedAdvanceSearchBtn = function (inNeedAdvanceSearchBtn) {
        this.needAdvanceSearchBtn = inNeedAdvanceSearchBtn;
    };

    /**
     * Determine whether or not need to display search textbox
     */
    this.needKeywordSearch = true;
    this.setNeedKeywordSearch = function (inNeedKeywordSearch) {
        this.needKeywordSearch = inNeedKeywordSearch;
    };

    /**
     * Foreign keys
     */ 
    this.searchForeignKeys = null;
    this.setSearchForeignKeys = function (inSearchForeignKeys) {
        this.searchForeignKeys = inSearchForeignKeys;
    };

    /**
     * Set ajax source
     */
    this.setAjaxSource = function (srcTableId, newVal) {
        var oTable = $(srcTableId).dataTable();
        var oSettings = oTable.fnSettings();
        oSettings.sAjaxSource = newVal;
        oTable.fnDraw();
    };

    /**
     * Remove specified record in table
     * srcTableId: Table identifier
     * recordId: record identifier
     */
    this.removeRecord = function (srcTableId, recordId) {
        var jTbl = $(srcTableId).DataTable();
        if (jTbl != null) {
            var row = jTbl.rows(function (idx, data, node) {
                // Compare identifier
                return data[0] == recordId ? true : false;
            });
            if (row != null) {
                // Send request to reload data in server side
                // false: not reset paging
                row.remove().draw(false);
            }
        }
    };

    /**
     * Create data table with options and column filters
     */
    this.initJDataTable = function () {
        // Create data table with options and column filters
        this.prepareLoadingDataFromServer();

        var customBtns = '';
        // Need to create AdvanceSearch button to header
        if (this.needAdvanceSearchBtn) {
            customBtns += '<button class="btn btn-success" style="margin-right: 10px;" ' +
                'onclick="' + this.enterAdvanceSearchFunctionJS + '"><span>' +
                this.localizedMessages.sAdvanceSearch + '</span></button>';
        }

        // Need to create AddNew button to header
        if (this.needDisplayAddNewBtn) {
            customBtns += '<button class="btn btn-primary" ' +
                'onclick="' + this.addNewFunctionJS + '"><span>' +
                this.localizedMessages.sAddNew + '</span></button>';
            
        }

        $(this.customButtonsAreaId).html(customBtns);
    };

    /**
     * Get records from server side using ajax
     * userOption: the options defined by user
     */
    this.prepareLoadingDataFromServer = function () {
        // Default options
        var opt = {
            "serverSide": true,
            "sAjaxSource": {
                "url": this.selectUrl
            },
            "sAjaxDataProp": "data",
            "processing": true,
            "oLanguage": {
                "sSearch": this.localizedMessages.sSearch,
                "sInfo": this.localizedMessages.sInfo,
                "sLengthMenu": this.localizedMessages.sLengthMenu,
                "sSearchKeyword": this.localizedMessages.sSearchKeyword,
                "sProcessing": "<div><img class='img-loader' src='../Templates/Admin/images/processing.gif' /></div>"
            },
            "bFilter": this.needKeywordSearch,
            "aoColumnDefs": this.aoColumnDefs,
            "aaSorting": this.aaSorting,
            "aoColumns": this.aoColumns,
            "aLengthMenu": this.aLengthMenu,
            "iDisplayLength": this.defaultDisplayLength,
            "sPaginationType": "full_numbers",
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                // Convert data for making corresponding with WebAPI
                var requestContext = {
                    Session: $.grep(aoData, function (e) { return e.name == "sEcho"; })[0].value,
                    StartRecordIndex: $.grep(aoData, function (e) { return e.name == "iDisplayStart"; })[0].value,
                    NumRecordsPerPage: $.grep(aoData, function (e) { return e.name == "iDisplayLength"; })[0].value,
                    SortColumnIndex: $.grep(aoData, function (e) { return e.name == "iSortCol_0"; })[0].value,
                    SortDirection: $.grep(aoData, function (e) { return e.name == "sSortDir_0"; })[0].value,
                };
                if (that.needKeywordSearch) {
                    requestContext.SearchKeyword = $.grep(aoData, function (e) { return e.name == "sSearch"; })[0].value;
                }
                requestContext.SearchForeignKeys = that.searchForeignKeys;

                // Execute ajax post to get data
                oSettings.jqXHR = $.ajax({
                    "dataType": 'json',
                    "url": sSource.url,
                    "type": 'POST',
                    "data": requestContext,
                    "success": function (response) {
                        //do error checking here, maybe "throw new Error('Some error')" based on data;
                        if (response.Result) {
                            // Convert data for making corresponding with jquery datatable
                            var responseContext = {
                                draw: response.Session,
                                recordsTotal: response.NumTotalRecords,
                                recordsFiltered: response.NumTotalRecords,
                                data: response.Records
                            };
                            fnCallback(responseContext);
                        } else {
                            app.logger.error(response.Description);
                        }
                    },
                    "error": function () { //404 errors and the like wil fall here
                        app.logger.error("Unexpected error in loading data from server");
                    }
                });
            },
            "fnCreatedRow": this.fnCreatedRow,
            "fnDrawCallback": this.fnDrawCallback
        };

        if (this.needDisplayColVis || this.needDisplayAddNewBtn || this.needAdvanceSearchBtn) {
            opt.sDom = '<"' + this.getCustomButtonsArea() + '"><"clear">C<"clear">lfrtip';
            opt.oColVis = {
                "buttonText": this.localizedMessages.sColVis
            };
        }

        var tbl = this.srcTable;

        if (tbl.hasClass("dataTable-noheader")) {
            opt.bFilter = false;
            opt.bLengthChange = false;
        }

        if (tbl.hasClass("dataTable-nofooter")) {
            opt.bInfo = false;
            opt.bPaginate = false;
        }

        if (tbl.hasClass("dataTable-nosort")) {
            var column = tbl.attr('data-nosort');
            column = column.split(',');
            for (var i = 0; i < column.length; i++) {
                column[i] = parseInt(column[i]);
            };
            opt.aoColumnDefs = [
                {
                    'bSortable': false,
                    'aTargets': column
                }
            ];
        }

        if (tbl.hasClass("dataTable-scroll-x")) {
            opt.sScrollX = "100%";
            opt.bScrollCollapse = true;
            $(window).resize(function () {
                oTable.fnAdjustColumnSizing();
            });
        }

        if (tbl.hasClass("dataTable-scroll-y")) {
            opt.sScrollY = "300px";
            opt.bPaginate = false;
            opt.bScrollCollapse = true;
            $(window).resize(function () {
                oTable.fnAdjustColumnSizing();
            });
        }

        if (tbl.hasClass("dataTable-reorder")) {
            opt.sDom = "R" + opt.sDom;
        }

        //if (tbl.hasClass("dataTable-colvis")) {
        //    opt.sDom = "C" + opt.sDom;
        //    opt.oColVis = {
        //        "buttonText": "Change columns <i class='icon-angle-down'></i>"
        //    };
        //}

        if (tbl.hasClass('dataTable-tools')) {
            opt.sDom = "T" + opt.sDom;
            opt.oTableTools = {
                "sSwfPath": "js/plugins/datatable/swf/copy_csv_xls_pdf.swf"
            };
        }

        if (tbl.hasClass("dataTable-scroller")) {
            opt.sScrollY = "300px";
            opt.bDeferRender = true;
            if (tbl.hasClass("dataTable-tools")) {
                opt.sDom = 'TfrtiS';
            } else {
                opt.sDom = 'frtiS';
            }
            opt.sAjaxSource = "js/plugins/datatable/demo.txt";
        }

        if (tbl.hasClass("dataTable-grouping") && tbl.attr("data-grouping") == "expandable") {
            opt.bLengthChange = false;
            opt.bPaginate = false;
        }

        // Create datatable with options and column filters
        var oTable = tbl.dataTable(opt).fnSetFilteringDelay(1000);

        //// Default column filters
        //var colFilters = {

        //};

        //// Merge user option to default
        //if (userColFilters != null) {
        //    $.extend(colFilters, userColFilters);
        //}

        //// Add column filters for data table
        //oTable.columnFilter(colFilters);

        tbl.css("width", '100%');
        $('.dataTables_filter input').attr("placeholder", opt.oLanguage.sSearchKeyword);
        $(".dataTables_length select").wrap("<div class='input-mini'></div>").chosen({
            disable_search_threshold: 9999999
        });

        $("#check_all").click(function (e) {
            $('input', oTable.fnGetNodes()).prop('checked', this.checked);
        });

        if (tbl.hasClass("dataTable-fixedcolumn")) {
            new FixedColumns(oTable);
        }

        //if (tbl.hasClass("dataTable-columnfilter")) {
        //    oTable.columnFilter({
        //        "sPlaceHolder": "head:after"
        //    });
        //}

        if (tbl.hasClass("dataTable-grouping")) {
            var rowOpt = {};

            if (tbl.attr("data-grouping") == 'expandable') {
                rowOpt.bExpandableGrouping = true;
            }
            oTable.rowGrouping(rowOpt);
        }

        //oTable.fnDraw();
        //oTable.fnAdjustColumnSizing();
    };
};
