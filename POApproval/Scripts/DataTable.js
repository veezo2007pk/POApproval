
(function ($) {
    "use strict";


    /*==================================================================
    [ Focus input ]*/
    $('.input100').each(function () {
        $(this).on('blur', function () {
            if ($(this).val().trim() != "") {
                $(this).addClass('has-val');
            }
            else {
                $(this).removeClass('has-val');
            }
        })
    })


    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .input100');

    $('.validate-form').on('submit', function () {
        var check = true;

        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }

        return check;
    });


    $('.validate-form .input100').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });

    function validate(input) {
        if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
            if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                return false;
            }
        }
        else {
            if ($(input).val().trim() == '') {
                return false;
            }
        }
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }

    /*==================================================================
    [ Show pass ]*/
    var showPass = 0;
    $('.btn-show-pass').on('click', function () {
        if (showPass == 0) {
            $(this).next('input').attr('type', 'text');
            $(this).find('i').removeClass('zmdi-eye');
            $(this).find('i').addClass('zmdi-eye-off');
            showPass = 1;
        }
        else {
            $(this).next('input').attr('type', 'password');
            $(this).find('i').addClass('zmdi-eye');
            $(this).find('i').removeClass('zmdi-eye-off');
            showPass = 0;
        }

    });
    $(document).ready(function () {
        $('#otherTable').DataTable({
            "ordering": true,
            "searching": true,
            "pagelength": 50,
            "paging": true,
            "stateSave": true,
        });

    var table=    $('#myTable').DataTable({
            "ordering": true,
            "searching": true,
        "pagelength": 50,
        "paging": true,
        "stateSave": true,
            columns: [
                { data: "" },
                { data: "PO Number" },
                { data: "PO Status" },
                { data: "Next" },
                { data: "Next PO Status" },
                { data: "Approval Level" },
                { data: "Supplier Code" },
                { data: "Supplier Name" },
                { data: "Date" },
                { data: "Buyer" },
               
                { data: "Qty", className: "qty" },
                { data: "Amount", className: "sum" },
                { data: "Action" }
        ],
        
            "footerCallback": function (row, data, start, end, display) {
                var api = this.api();

                api.columns('.sum', { page: 'current' }).every(function () {
                    var sum = this
                        .data()
                        .reduce(function (a, b) {
                            var x = parseFloat(a) || 0;
                            var y = parseFloat(b) || 0;
                            return x + y;
                        }, 0);
                    console.log(sum); //alert(sum);
                    //$(this.footer()).html(sum);
                    $('#totalnormal span').html(sum.toFixed(2));
                });

                api.columns('.qty', { page: 'current' }).every(function () {
                    var qty = this
                        .data()
                        .reduce(function (a, b) {
                            var x = parseFloat(a) || 0;
                            var y = parseFloat(b) || 0;
                            return x + y;
                        }, 0);
                    $('#totalQtynormal span').html(qty);
                    //console.log("totalsum"+sum);
                    console.log(qty); //alert(sum);
                    //$(this.footer()).html(sum);
                    //$('#total span').html(sum);
                });
            }
    });
        $('#myform').on('submit', function (e) {

            var form = this;


            // Encode a set of form elements from all pages as an array of names and values
            var params = table.$('input,select,textarea').serializeArray();

            // Iterate over all form elements
            $.each(params, function () {
                // If element doesn't exist in DOM
                if (!$.contains(document, form[this.name])) {
                    // Create a hidden element
                    $(form).append(
                        $('<input>')
                            .attr('type', 'hidden')
                            .attr('name', this.name)
                            .val(this.value)
                    );
                }
            });
        });

        
        //$('#mytablepo').DataTable({

        //    "ordering": true,
        //    "searching": true,

        //    "pagelength": 50,
        //    //drawcallback: function () {
        //    //    var sum = $('#mytable').datatable().column(11).data().sum();
        //    //    $('#total').html(sum);
        //    //}
        //});
        //var sum = $('#myTablePO').DataTable().column(10).data().sum();
        //alert(sum);
        //$('#total').html(sum);

        //$('#myTablePO').DataTable({
        //    drawCallback: function () {
        //        var api = this.api();
        //        $(api.table().footer()).html(
                    
        //            api.column(10, { page: 'current' }).nodes().sum()
        //        );
        //    }
        //});

        
    });

    

    $(document).ready(function () {

        var table=    $('#myTablePO').DataTable({
            "ordering": true,
            "searching": true,
            "pagelength": 50,
            "paging": true,
          "stateSave": true,
          
            columns: [
                { data: "" },
                { data: "PO Number" },
                { data: "PO Status" },
                { data: "Next" },
                { data: "Next PO Status" },
                { data: "Approval Level" },
                { data: "Supplier Code" },
                { data: "Supplier Name" },
                { data: "Date" },
                { data: "Buyer" },
                { data: "Qty", className: "qty" },
                { data: "Amount", className: "sum" },
                { data: "Action" }
            ],
            "footerCallback": function (row, data, start, end, display) {
                var api = this.api();

                api.columns('.sum', { page: 'current' }).every(function () {
                    var sum = this
                        .data()
                        .reduce(function (a, b) {
                            var x = parseFloat(a) || 0;
                            var y = parseFloat(b) || 0;
                            return x + y;
                        }, 0);
                    var finalsum = sum.toLocaleString('en-US', { maximumFractionDigits: 2 });
                    //console.log(sum); //alert(sum);
                    //$(this.footer()).html(sum);
                    $('#total span').html(finalsum);
                });

                api.columns('.qty', { page: 'current' }).every(function () {
                    var qty = this
                        .data()
                        .reduce(function (a, b) {
                            var x = parseFloat(a) || 0;
                            var y = parseFloat(b) || 0;
                            return x + y;
                        }, 0);
                    var finalqty=qty.toLocaleString('en-US', { maximumFractionDigits: 0 });
                    $('#totalQty span').html(finalqty);
                    //console.log("totalsum"+sum);
                    console.log(qty); //alert(sum);
                    //$(this.footer()).html(sum);
                    //$('#total span').html(sum);
                });
            }
        });
        $('#myform').on('submit', function (e) {

            var form = this;
           

            // Encode a set of form elements from all pages as an array of names and values
            var params = table.$('input,select,textarea').serializeArray();

            // Iterate over all form elements
            $.each(params, function () {
                // If element doesn't exist in DOM
                if (!$.contains(document, form[this.name])) {
                    // Create a hidden element
                    $(form).append(
                        $('<input>')
                            .attr('type', 'hidden')
                            .attr('name', this.name)
                            .val(this.value)
                    );
                }
            });
        });
        //var creditAmount = 0
        //$("#myTablePO").on('change', function () {

        //    var checkedCount = $("#myTablePO input:checked").length;

        //    for (var i = 0; i < checkedCount; i++) {

        //        var amount = $(this).find('td:eq(10)').text();
        //        alert(amount);
        //        if (amount != "") {
        //            creditAmount += parseFloat(amount);
        //        } else {
        //            creditAmount = 0;
        //        }
        //    }
        //    $("#idSmofAmount").text(creditAmount);

        //});

        var creditAmount = 0
        var creditQty = 0
        $("#myTablePO").on('change', function () {
           
            var checkedCount = $("#myTablePO input:checked").length;
            //console.log(checkedCount);
            var creditAmount = 0
            var creditQty = 0
            $("#idSmofAmount").text(0);
            for (var i = 0; i < checkedCount; i++) {
                var qty = $("#myTablePO input:checked")[i].parentNode.parentNode.parentNode.children[10].innerHTML;
                var amount = $("#myTablePO input:checked")[i].parentNode.parentNode.parentNode.children[11].innerHTML;
                if (amount != "") {
                    creditAmount += parseFloat(amount);
                } else {
                    creditAmount = 0;
                }

                if (qty != "") {
                    creditQty += parseFloat(qty);
                } else {
                    creditQty = 0;
                }
            }
            var grandtotal = creditAmount.toLocaleString('en-US', { maximumFractionDigits: 2 })
            $("#total span").text(grandtotal);

            

            var grandqty = creditQty.toLocaleString('en-US', { maximumFractionDigits: 0 });
            $("#totalQty span").text(grandqty);

            
        });
        $("#myTable").on('change', function () {

            var checkedCount = $("#myTable input:checked").length;
            //console.log(checkedCount);
            var creditAmount = 0
            var creditQty = 0
            $("#idSmofAmount").text(0);
            for (var i = 0; i < checkedCount; i++) {
                var qty = $("#myTable input:checked")[i].parentNode.parentNode.parentNode.children[10].innerHTML;
                var amount = $("#myTable input:checked")[i].parentNode.parentNode.parentNode.children[11].innerHTML;
                if (amount != "") {
                    creditAmount += parseFloat(amount);
                } else {
                    creditAmount = 0;
                }

                if (qty != "") {
                    creditQty += parseFloat(qty);
                } else {
                    creditQty = 0;
                }
            }
            var grandtotal = creditAmount.toLocaleString('en-US', { maximumFractionDigits: 2 })
            $("#totalnormal span").text(grandtotal);



            var grandqty = creditQty.toLocaleString('en-US', { maximumFractionDigits: 0 });
            $("#totalQtynormal span").text(grandqty);


        });

    });

})(jQuery);