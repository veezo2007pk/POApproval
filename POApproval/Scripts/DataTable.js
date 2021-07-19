
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
        });

        $('#myTable').DataTable({
            "ordering": true,
            "searching": true,
            "pagelength": 50000,
            "paging": false,
            columns: [
                { data: "" },
                { data: "PO Number" },
                { data: "PO Status" },
                { data: "Next" },
                { data: "Next PO Status" },
              /*  { data: "Approval Level" },*/
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

        $('#myTablePO').DataTable({
            "ordering": true,
            "searching": true,
            "pagelength": 50000,
            "paging":false,
            columns: [
                { data: "" },
                { data: "PO Number" },
                { data: "PO Status" },
                { data: "Next" },
                { data: "Next PO Status" },
              
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
                    $('#total span').html(sum.toFixed(2));
                });

                api.columns('.qty', { page: 'current' }).every(function () {
                    var qty = this
                        .data()
                        .reduce(function (a, b) {
                            var x = parseFloat(a) || 0;
                            var y = parseFloat(b) || 0;
                            return x + y;
                        }, 0);
                    $('#totalQty span').html(qty);
                    //console.log("totalsum"+sum);
                    console.log(qty); //alert(sum);
                    //$(this.footer()).html(sum);
                    //$('#total span').html(sum);
                });
            }
        });


        //var creditAmount = 0
        // $('#firstTable').DataTable();

    //    $("#myTablePO").on('change', function () {
    //        //alert('fffff');
    //        var checkedCount = $("#myTablePO input:checked").length;
    //        //alert(checkedCount);
    //        for (var i = 0; i < checkedCount; i++) {
    //            var htmldata = $("#myTablePO input:checked")[i];
    //            console.log(htmldata);
    //        }
    //        //var creditAmount = 0

    //        //for (var i = 0; i < checkedCount; i++) {

    //        //    var amount = $("#myTablePO input:checked")[i].parentNode.parentNode.children[10].innerHTML

    //        //    if (amount != "") {
    //        //        creditAmount += parseFloat(amount);
    //        //    } else {
    //        //        creditAmount = 0;
    //        //    }
    //        //}

    //        //$("#idSmofAmount").text(creditAmount);

    //    });
    //});

    //$(".datepicker input").datepicker({
    //    autoclose: true,
    //    todayHighlight: true
    //}).datepicker('update', new Date());

})(jQuery);