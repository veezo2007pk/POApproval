
var app = angular.module("myPO", [])
    .directive('loading', ['$http', function ($http) {
        return {
            restrict: 'A',
            template: '<div class="loading-spiner"><img src="http://www.nasa.gov/multimedia/videogallery/ajax-loader.gif" /> </div>',
            link: function (scope, elm, attrs) {
                scope.isLoading = function () {
                    return $http.pendingRequests.length > 0;
                };

                scope.$watch(scope.isLoading, function (v) {
                    if (v) {
                        elm.show();
                    } else {
                        elm.hide();
                    }
                });
            }
        };
    }])

    .controller("POCtrl", function ($scope, $http) {

        $scope.SaearchPO = function () {
            console.log($scope.strPOStatus + "" + $scope.PO_Number)
            $http({
                method: "POST",
                url: "http://localhost:61646/PO/SearchPO",
                dataType: 'json',
                data: { strPOStatus: $scope.strPOStatus, PO_Number: $scope.PO_Number },
                headers: { "Content-Type": "application/json" }
            }).then(function (data) {

                $scope.POs = data.data;
            })
        }


        //debugger;
    });  


$("#SaearchPO").click(function () {
    //alert("");  
   
  
    LoadData();
});  
function dateFormat(d) {
    console.log(d);
    return ((d.getDate() + 1) + "").padStart(2, "0")
        + "-" + (d.getMonth() + "").padStart(2, "0")
        + "-" + d.getFullYear();
}
function LoadData() {
   
    $("#myTable tbody").empty();
    var strPOStatus = $("#strPOStatus").val();
    var PO_Number = $("#PO_Number").val();
    $.ajax({
        type: 'POST',
        url: "http://localhost:61646/PO/SearchPO",
        dataType: 'json',
        data: { strPOStatus: strPOStatus, PO_Number: PO_Number },
        success: function (data) {
         
            var items = '';
            $.each(data, function (i, item) {
                
                console.log(item);
                var date = new Date(item.Creation_Date).toString();
                console.log(date);
                var newDate = date.toString('dd-MM-yy');
                var rows = "<tr>"
                    + "<td><a href = /PO/ReviewPO?ID="+item.intPOCode+" value='Update' HR> View</a></td>"
                    + "<td>" + item.PO_Number + "</td>"
                    + "<td>" + item.Supplier_Code + "</td>"
                    + "<td>" + item.Supplier_Name + "</td>"
                    + "<td>" + dateFormat(new Date(parseInt((item.Creation_Date).match(/\d+/)[0])))  + "</td>"
                    + "<td>" + item.strPOStatus + "</td>"
                    + "<td>" + item.Buyer + "</td>"
                    + "</tr>";
                $('#myTable tbody').append(rows);
             
            });
            $('#myTable').DataTable({
                //"destroy": true, //use for reinitialize datatable
                "paging": true,
                "info": false,
                "retrieve": true,

            });
        
               
           
        },
        error: function (ex) {
            var r = jQuery.parseJSON(response.responseText);
            alert("Message: " + r.Message);
            alert("StackTrace: " + r.StackTrace);
            alert("ExceptionType: " + r.ExceptionType);
            
        }
      
    });
    
    return false;
  
}  