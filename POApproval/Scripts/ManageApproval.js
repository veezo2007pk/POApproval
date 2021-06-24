
var app = angular.module("myManageApprovalApp", [])
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

    .controller("manageApprovalCtrl", function ($scope, $http) {

        $scope.GetUser = function () {
            $http({
                method: "POST",
                url: "http://localhost:61646/ManageApproval/GetUser/",
                dataType: 'json',
                data: {},
                headers: { "Content-Type": "application/json" }
            }).then(function (data) {
                $scope.DefaultLabel = "Select Department";
                $scope.Users = data.data;
            })
        }
        $scope.GetApprovalLevel = function () {
            $http({
                method: "POST",
                url: "http://localhost:61646/ManageApproval/GetApprovalLevel/",
                dataType: 'json',
                data: {},
                headers: { "Content-Type": "application/json" }
            }).then(function (data) {
                $scope.DefaultLabel = "Select Department";
                $scope.ApprovalLevels = data.data;
            })
        }


        //debugger;
        $scope.InsertData = function () {
            var Action = document.getElementById("btnSave").getAttribute("value");
           
           
            if (Action == "Submit") {
                if (confirm('Are you sure you want to insert this?')) {
                    $("#btnSave").attr("disabled", true);
                    $scope.ManageApproval = {};
                    $scope.ManageApproval.intUserCode = $scope.intUserCode;
                    $scope.ManageApproval.intApprovalLevelCode = $scope.intApprovalLevelCode;
                    $scope.ManageApproval.numFromApprovalAmount = $scope.numFromApprovalAmount;
                    $scope.ManageApproval.numToApprovalAmount = $scope.numToApprovalAmount;
                    $scope.ManageApproval.bolIsActive = $scope.bolIsActive;
                    if ($scope.ManageApproval.numFromApprovalAmount > $scope.ManageApproval.numToApprovalAmount) {
                        alert('Approval From amount must be less than Approval To amount');
                        return;
                    }
                    $http({
                        method: "post",
                        url: "http://localhost:61646/ManageApproval/Insert_ManageApproval",
                        datatype: "json",
                        data: JSON.stringify($scope.ManageApproval)
                    }).then(function (response) {

                        if (response.data == '2') {
                            $("#btnSave").attr("disabled", false);
                            $("#UserErrorMessage").text("Approval level for this user is already exist!");
                            return;
                            //document.getElementsByClassName('UserErrorMessage').='Username already exist'
                        }
                        else {
                            alert('Manage Approval added successfully');
                            $scope.GetAllData();
                            $scope.intUserCode = "";
                            $scope.intApprovalLevelCode = "";
                            $scope.numFromApprovalAmount = "";
                            $scope.numToApprovalAmount = "";
                            $scope.bolIsActive = "";
                            window.location.href = "/poapproval/ManageApproval/ManageApprovalList"
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
                    $scope.ManageApproval = {};
                    $scope.ManageApproval.intManageApprovalCode = $scope.intManageApprovalCode;
                    $scope.ManageApproval.intUserCode = $scope.intUserCode;
                    $scope.ManageApproval.intApprovalLevelCode = $scope.intApprovalLevelCode;
                    $scope.ManageApproval.numFromApprovalAmount = $scope.numFromApprovalAmount;
                    $scope.ManageApproval.numToApprovalAmount = $scope.numToApprovalAmount;
                    $scope.ManageApproval.bolIsActive = $scope.bolIsActive;
                    $scope.ManageApproval.dtCreatedAt = $scope.dtCreatedAt;
                    $scope.ManageApproval.intCreatedByCode = $scope.intCreatedByCode;
                    $scope.ManageApproval.dtModifyAt = $scope.dtModifyAt;
                    $scope.ManageApproval.intModifyByCode = $scope.intModifyByCode;
                    if ($scope.ManageApproval.numFromApprovalAmount > $scope.ManageApproval.numToApprovalAmount) {
                        alert('Approval From amount must be less than Approval To amount');
                        return;
                    }
                    $http({
                        method: "post",
                        url: "http://localhost:61646/ManageApproval/Update_ManageApproval",
                        datatype: "json",
                        data: JSON.stringify($scope.ManageApproval)
                    }).then(function (response) {
                        if (response.data == '2') {
                            $("#btnSave").attr("disabled", false);
                            $("#UserErrorMessage").text("Approval level for this user is already exist!");
                            return;
                            //document.getElementsByClassName('UserErrorMessage').='Username already exist'
                        }
                        else {
                            alert('Manage Approval updated successfully');
                            $scope.GetAllData();
                            $scope.intUserCode = "";
                            $scope.intApprovalLevelCode = "";
                            $scope.numFromApprovalAmount = "";
                            $scope.numToApprovalAmount = "";
                            $scope.bolIsActive = "";
                            window.location.href = "/poapproval/ManageApproval/ManageApprovalList"
                        }

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
                url: "http://localhost:61646/ManageApproval/Get_AllManageApproval"
            }).then(function (response) {
                $scope.ManageApprovals = response.data;
            }, function () {
              
            })
        };
        $scope.DeleteEmp = function (Emp) {
            $http({
                method: "post",
                url: "http://localhost:61646/ManageApproval/Delete_ManageApproval",
                datatype: "json",
                data: JSON.stringify(Emp)
            }).then(function (response) {
               
                $scope.GetAllData();
            })
        };
        $scope.UpdateManageApproval = function (ManageApproval) {
            console.log(ManageApproval);
            //document.getElementById("EmpID_").value = Emp.Emp_Id;
            //$scope.EmpName = Emp.Emp_Name;
            //$scope.EmpCity = Emp.Emp_City;
            //$scope.EmpAge = Emp.Emp_Age;
            //document.getElementById("btnSave").setAttribute("value", "Update");
            //document.getElementById("btnSave").style.backgroundColor = "Yellow";
            //document.getElementById("spn").innerHTML = "Update ManageApproval Information";
            $scope.intManageApprovalCode = ManageApproval.intManageApprovalCode;
            $scope.intUserCode = ManageApproval.intUserCode;
            $scope.intApprovalLevelCode = ManageApproval.intApprovalLevelCode;
            $scope.numFromApprovalAmount = ManageApproval.numFromApprovalAmount;
            $scope.numToApprovalAmount = ManageApproval.numToApprovalAmount;
            $scope.bolIsActive = ManageApproval.bolIsActive;
            $scope.dtCreatedAt = ManageApproval.dtCreatedAt;
            $scope.intCreatedByCode = ManageApproval.intCreatedByCode;
            $scope.dtModifyAt = ManageApproval.dtModifyAt;
            $scope.intModifyByCode = ManageApproval.intModifyByCode;
            $scope.GetUser();
            $scope.GetApprovalLevel();
        }
    });  