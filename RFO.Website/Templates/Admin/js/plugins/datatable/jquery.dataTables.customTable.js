/**
 * Create data table with user customization
 * @param tbl: the table contains data loaded from server side
 * @param userOption: the options defined by user
 * @param userColFilters: the user column filters defined by user
 */
function createDataTable(tbl, userOption) {
    // Default options
    var opt = {
        "serverSide": true,
        "sAjaxDataProp": "data",
        "processing": true,
        "sPaginationType": "full_numbers",
        //"sDom": 'C<"clear">lfrtip',
        "sDom": '<"addNewArea"><"clear">C<"clear">lfrtip',
        "oColVis": {
            "buttonText": "Tùy chỉnh cột hiển thị"
        },
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "url": sSource.url,
                "data": aoData,
                "success": function (response) {
                    //do error checking here, maybe "throw new Error('Some error')" based on data;
                    if (response.IsSuccessful != null && !response.IsSuccessful) {
                        app.logger.error(response.Message);
                    }
                    fnCallback(response);
                },
                "error": function () { //404 errors and the like wil fall here
                    app.logger.error("Unexpected error in loading data from server");
                }
            });
        }
    };

    // Merge user option to default
    if (userOption != null) {
        $.extend(opt, userOption);
    }

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

    if (tbl.hasClass("dataTable-colvis")) {
        opt.sDom = "C" + opt.sDom;
        opt.oColVis = {
            "buttonText": "Change columns <i class='icon-angle-down'></i>"
        };
    }

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
    var oTable = tbl.dataTable(opt).fnSetFilteringDelay(500);

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
}
