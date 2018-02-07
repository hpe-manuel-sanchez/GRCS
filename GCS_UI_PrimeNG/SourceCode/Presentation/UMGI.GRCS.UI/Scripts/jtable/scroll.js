(function ($) {
    $.fn.Scrollable = function (options) {
//        var defaults = {
//            ScrollHeight: 300,
//            Width: 0
//        };
//        var options = $.extend(defaults, options);
        return this.each(function () {
            var grid = $(this).get(0);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }


            //            if (document.getElementById("tempDiv") != null) {
            //                var divElem = document.getElementById("tempDiv");
            //                var parentElement = divElem.parentNode;
            //                parentElement.removeChild(divElem);
            //            }
            var dummyHeader = null;
            var divElement = document.createElement("div");
            divElement.setAttribute('id', 'tempDiv');
            grid.parentNode.appendChild(divElement);
            var parentDiv = grid.parentNode;

            //            if (document.getElementById("dummyHeader") != null) {
            //                var divElem = document.getElementById("dummyHeader");
            //                var parentElement = divElem.parentNode;
            //                parentElement.removeChild(divElem);
            //            }

            //            if (document.getElementById("tempTable") != null) {
            //                var divElem = document.getElementById("tempTable");
            //                var parentElement = divElem.parentNode;
            //                parentElement.removeChild(divElem);
            //            }

            if ($('#tempTable').length == 0) {
                var table = document.createElement("table");
                table.setAttribute('id', 'tempTable');
                table.setAttribute('class', 'jtable');
                for (i = 0; i < grid.attributes.length; i++) {
                    if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                        table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                    }
                }
                table.style.cssText = grid.style.cssText;
                table.style.width = gridWidth + "px";

                //            if (document.getElementById("tempTableBody") != null) {
                //                var divElem = document.getElementById("tempTableBody");
                //                var parentElement = divElem.parentNode;
                //                parentElement.removeChild(divElem);
                //            }
                var tableBody = document.createElement("tbody");
                tableBody.setAttribute('id', 'tempTableBody');
                table.appendChild(tableBody);
                table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
                var cells = table.getElementsByTagName("TH");

                var gridRow = grid.getElementsByTagName("TR")[0];
                for (var i = 0; i < cells.length; i++) {
                    var width;
                    if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                        width = headerCellWidths[i];
                    }
                    else {
                        width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                    }
                    cells[i].style.width = parseInt(width - 5) + "px";
                    gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
                }
                parentDiv.removeChild(grid);

                //            if (document.getElementById("dummyHeader") != null) {
                //                var divElem = document.getElementById("dummyHeader");
                //                var parentElement = divElem.parentNode;
                //                parentElement.removeChild(divElem);
                //            }

                dummyHeader = document.createElement("div");
                dummyHeader.setAttribute('id', 'dummyHeader');
                dummyHeader.appendChild(table);
                parentDiv.appendChild(dummyHeader);
                if (options.Width > 0) {
                    gridWidth = options.Width;
                }
            }
            else
                dummyHeader = document.getElementById("scrollableDiv");

            //            if (document.getElementById("scrollableDiv") != null) {
            //                var divElem = document.getElementById("scrollableDiv");
            //                var parentElement = divElem.parentNode;
            //                parentElement.removeChild(divElem);
            //            }

            var scrollableDiv = document.createElement("div");
            dummyHeader.setAttribute('id', 'scrollableDiv');
            if (parseInt(gridHeight) > options.ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }


            scrollableDiv.style.cssText = "overflow:auto;height:" + options.ScrollHeight + "px;width:" + gridWidth + "px";            
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
            //     $("#StudentTableContainer th").addClass("jtable-column-header jtable-column-header-selecting");
//            $('#tempTable').find('tbody tr:th').each(function (index) {
//                $(tbodyElements[index]).width(this.style.width);
//            });
          
          //  $(grid).find('th').addClass('jtable-command-column-header');
            if ($('#bottom-panel').length > 0) {
                var bottomPanel = document.getElementById("bottom-panel");
                if (document.getElementById("bottom-panel-div") != null) {
                    var divElem = document.getElementById("bottom-panel-div");
                    var parentElement = divElem.parentNode;
                    parentElement.removeChild(divElem);
                }

                var bottomPanelDiv = document.createElement("div");
                bottomPanelDiv.setAttribute('id', 'bottom-panel-div');
                bottomPanelDiv.appendChild(bottomPanel);
                var parentFooter = dummyHeader.parentNode;
                // var parent = parentDiv.parentNode;              
                parentFooter.appendChild(bottomPanelDiv);
            }
            $('#tempTable tbody tr th').addClass("jtable-column-header jtable-column-header-selecting");
        });
    };
})(jQuery);
