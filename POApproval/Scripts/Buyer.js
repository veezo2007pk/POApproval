
var app = angular.module("buyerApp", [])
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

    .controller("buyerCtrl", function ($scope, $http) {

        $scope.GetDepartment = function () {
          
            $http({
                method: "POST",
                url: "http://10.3.1.110/poapproval/Buyer/GetDepartment/",
                dataType: 'json',
                data: { ID: $scope.strDepartmentName },
                headers: { "Content-Type": "application/json" }
            }).then(function (data) {
                $scope.DefaultLabel = "Select Department";
                $scope.Departments = data.data;
                $("#wait").css("display", "none");
            })
        }


        //debugger;
        $scope.InsertData = function () {
           
            var Action = document.getElementById("btnSave").getAttribute("value");
            if (Action == "Submit") {
                if (confirm('Are you sure you want to insert this?')) {
                    $("#btnSave").attr("disabled", true);
                    $scope.Buyer = {};
                    $scope.Buyer.strBuyerName = $scope.strBuyerName;                   
                    $scope.Buyer.bolIsActive = $scope.bolIsActive;
                    $http({
                        method: "post",
                        url: "http://10.3.1.110/poapproval/Buyer/Insert_Buyer",
                        datatype: "json",
                        data: JSON.stringify($scope.Buyer)
                    }).then(function (response) {

                        if (response.data == '2') {
                            $("#btnSave").attr("disabled", false);
                            $("#BuyerErrorMessage").text("Buyer already exist!");
                            return;
                            //document.getElementsByClassName('BuyerErrorMessage').='Buyername already exist'
                        }
                       
                        else {
                            alert('Buyer added successfully');
                            $scope.GetAllData();
                            $scope.strBuyerName = "";                        
                            $scope.bolIsActive = "";
                            $("#wait").css("display", "none");
                            $("#BuyerErrorMessage").text("");
                            window.location.href = "/poapproval/Buyer/BuyerList"
                        }
                    }, function (error) {
                            $("#btnSave").attr("disabled", false);
                        alert('Something went wrong, please try again');
                    })
                }
            }
            if (Action == "Update") {
                if (confirm('Are you sure you want to update this?')) {
                    $("#btnSave").attr("disabled", true);
                    $scope.Buyer = {};
                    $scope.Buyer.intBuyerCode = $scope.intBuyerCode;
                    $scope.Buyer.strBuyerName = $scope.strBuyerName;
                    $scope.Buyer.bolIsActive = $scope.bolIsActive;
                    //$scope.Buyer.dtCreatedAt = $scope.dtCreatedAt;
                    //$scope.Buyer.intCreatedByCode = $scope.intCreatedByCode;
                    //$scope.Buyer.dtModifyAt = $scope.dtModifyAt;
                    //$scope.Buyer.intModifyByCode = $scope.intModifyByCode;

                    $http({
                        method: "post",
                        url: "http://10.3.1.110/poapproval/Buyer/Update_Buyer",
                        datatype: "json",
                        data: JSON.stringify($scope.Buyer)
                    }).then(function (response) {
                       
                        //if (response.data == '2') {

                        //    $("#BuyerErrorMessage").text("Buyername already exist!");
                        //    return;
                        //    //document.getElementsByClassName('BuyerErrorMessage').='Buyername already exist'
                        //}
                       
                      /*  else {*/
                            alert('Buyer updated successfully');
                            $scope.GetAllData();
                            $scope.strBuyerName = "";
                            $scope.bolIsActive = "";
                            window.location.href = "/poapproval/Buyer/BuyerList"
                        //}


                    }, function (error) {
                            $("#wait").css("display", "none");
                            $("#btnSave").attr("disabled", false);
                            alert('Something went wrong, please try again');
                    })
                }
            }
        }
        $scope.GetAllData = function () {
            $http({
                method: "get",
                url: "http://10.3.1.110/poapproval/Buyer/Get_AllBuyer"
            }).then(function (response) {
                $scope.Buyers = response.data;
            }, function () {
               
            })
        };
        $scope.DeleteEmp = function (Emp) {
            $http({
                method: "post",
                url: "http://10.3.1.110/poapproval/Buyer/Delete_Buyer",
                datatype: "json",
                data: JSON.stringify(Emp)
            }).then(function (response) {
               // alert(response.data);
                $scope.GetAllData();
            })
        };
        $scope.UpdateBuyer = function (Buyer) {
            console.log(Buyer);
            //document.getElementById("EmpID_").value = Emp.Emp_Id;
            //$scope.EmpName = Emp.Emp_Name;
            //$scope.EmpCity = Emp.Emp_City;
            //$scope.EmpAge = Emp.Emp_Age;
            //document.getElementById("btnSave").setAttribute("value", "Update");
            //document.getElementById("btnSave").style.backgroundColor = "Yellow";
            //document.getElementById("spn").innerHTML = "Update Buyer Information";

            $scope.intBuyerCode = Buyer.intBuyerCode;
            $scope.intBuyerCode = Buyer.intBuyerCode;
            $scope.strBuyerName = Buyer.strBuyerName;
            $scope.bolIsActive = Buyer.bolIsActive;
            //$scope.dtCreatedAt = Buyer.dtCreatedAt;
            //$scope.intCreatedByCode = Buyer.intCreatedByCode;
            //$scope.dtModifyAt = Buyer.dtModifyAt;
            //$scope.intModifyByCode = Buyer.intModifyByCode;
            //$scope.GetDepartment();
        }
    });