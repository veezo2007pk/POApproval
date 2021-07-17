
var app = angular.module("myBuyerManagerApp", [])
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

    .controller("BuyerManagerCtrl", function ($scope, $http) {

        $scope.GetUser = function () {
            $http({
                method: "GET",
                url: "http://localhost:61646/BuyerManager/GetUser/",
                dataType: 'json',
                data: {},
                headers: { "Content-Type": "application/json" }
            }).then(function (data) {
               
                $scope.Users = data.data;
            })
        }
        $scope.GetBuyer = function () {
            $http({
                method: "GET",
                url: "http://localhost:61646/BuyerManager/GetBuyer/",
                dataType: 'json',
                data: {},
                headers: { "Content-Type": "application/json" }
            }).then(function (data) {
                $scope.DefaultLabel = "Select Department";
                $scope.buyers = data.data;
            })
        }


        //debugger;
        $scope.InsertData = function () {
            var Action = document.getElementById("btnSave").getAttribute("value");
           
           
            if (Action == "Submit") {
                if (confirm('Are you sure you want to insert this?')) {
                    $("#btnSave").attr("disabled", true);
                    $scope.BuyerManager = {};
                    $scope.BuyerManager.intUserCode = $scope.intUserCode;
                    $scope.BuyerManager.intBuyerCode = $scope.intBuyerCode;
                   
                    $http({
                        method: "post",
                        url: "http://localhost:61646/BuyerManager/Insert_BuyerManager",
                        datatype: "json",
                        data: JSON.stringify($scope.BuyerManager)
                    }).then(function (response) {

                        if (response.data == '2') {
                            $("#btnSave").attr("disabled", false);
                            $("#UserErrorMessage").text("User with same buyer is already exist!");
                            return;
                            //document.getElementsByClassName('UserErrorMessage').='Username already exist'
                        }
                        else {
                            alert('Data added successfully');
                            $scope.GetAllData();
                        $scope.intUserCode = "";
                        $scope.intBuyerCode = "";
                         
                            window.location.href = "/poapproval/BuyerManager/BuyerManagerList"
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
                    $scope.BuyerManager = {};
                    $scope.BuyerManager.intBuyerDetailCode = $scope.intBuyerDetailCode;
                    $scope.BuyerManager.intBuyerCode = $scope.intBuyerCode;
                    $scope.BuyerManager.intUserCode = $scope.intUserCode;
                   
                    $scope.BuyerManager.dtCreatedAt = $scope.dtCreatedAt;
                    $scope.BuyerManager.intCreatedByCode = $scope.intCreatedByCode;
                    $scope.BuyerManager.dtModifyAt = $scope.dtModifyAt;
                    $scope.BuyerManager.intModifyByCode = $scope.intModifyByCode;
                    
                    $http({
                        method: "post",
                        url: "http://localhost:61646/BuyerManager/Update_BuyerManager",
                        datatype: "json",
                        data: JSON.stringify($scope.BuyerManager)
                    }).then(function (response) {
                        if (response.data == '2') {
                            $("#btnSave").attr("disabled", false);
                            $("#UserErrorMessage").text("User with same buyer is already exist!");
                            return;
                            //document.getElementsByClassName('UserErrorMessage').='Username already exist'
                        }
                        else {
                            alert('Data updated successfully');
                            $scope.GetAllData();
                            $scope.intUserCode = "";
                            $scope.intBuyerCode = "";

                            window.location.href = "/poapproval/BuyerManager/BuyerManagerList"
                        }
                       /* }*/

                    }, function (error) {
                        $("#btnSave").attr("disabled", false);
                        alert('Something went wrong, please try again');
                    })
                }
            }
        }
        $scope.GetAllData = function () {
            $http({
                method: "get",
                url: "http://localhost:61646/BuyerManager/Get_AllBuyerManager"
            }).then(function (response) {
                $scope.BuyerManagers = response.data;
            }, function () {
              
            })
        };
        $scope.DeleteBuyer = function (intBuyerDetailCode) {
            if (confirm('Are you sure you want to delete this?')) {
                $scope.BuyerManager = {};
                $scope.BuyerManager.intBuyerDetailCode = intBuyerDetailCode;
                $http({
                    method: "post",
                    url: "http://localhost:61646/BuyerManager/Delete_BuyerManager",
                    datatype: "json",
                    data: JSON.stringify($scope.BuyerManager)
                }).then(function (response) {
                    alert('Buyer Deleted successfully');
                    window.location.href = "/poapproval/BuyerManager/BuyerManagerList"
                })
            }
        };
        $scope.DeleteEmp = function (Emp) {
            $http({
                method: "post",
                url: "http://localhost:61646/BuyerManager/Delete_BuyerManager",
                datatype: "json",
                data: JSON.stringify(Emp)
            }).then(function (response) {
               
                $scope.GetAllData();
            })
        };
        $scope.UpdateBuyerManager = function (BuyerManager) {
            console.log(BuyerManager);
            //document.getElementById("EmpID_").value = Emp.Emp_Id;
            //$scope.EmpName = Emp.Emp_Name;
            //$scope.EmpCity = Emp.Emp_City;
            //$scope.EmpAge = Emp.Emp_Age;
            //document.getElementById("btnSave").setAttribute("value", "Update");
            //document.getElementById("btnSave").style.backgroundColor = "Yellow";
            //document.getElementById("spn").innerHTML = "Update BuyerManager Information";
            $scope.intBuyerDetailCode = BuyerManager.intBuyerDetailCode;
            $scope.intUserCode = BuyerManager.intUserCode;
            $scope.intBuyerCode = BuyerManager.intBuyerCode;
            $scope.dtCreatedAt = BuyerManager.dtCreatedAt;
            $scope.intCreatedByCode = BuyerManager.intCreatedByCode;
            $scope.dtModifyAt = BuyerManager.dtModifyAt;
            $scope.intModifyByCode = BuyerManager.intModifyByCode;
            $scope.GetUser();
            $scope.GetBuyer();
        }
    });  