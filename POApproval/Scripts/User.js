
var app = angular.module("myApp", [])
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

    .controller("myCtrl", function ($scope, $http) {

        $scope.GetDepartment = function () {
          
            $http({
                method: "POST",
                url: "http://localhost:61646/User/GetDepartment/",
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
            //alert('dddd');
            var Action = document.getElementById("btnSave").getAttribute("value");
            if (Action == "Submit") {
                if (confirm('Are you sure you want to insert this?')) {
                    $("#btnSave").attr("disabled", true);
                    $scope.User = {};
                    $scope.User.usercode = $scope.usercode;
                    $scope.User.fullname = $scope.fullname;
                    $scope.User.pwd = $scope.pwd;
                    $scope.User.email = $scope.email;
                    $scope.User.xpertLoginID = $scope.xpertLoginID;
                    $scope.User.usergroup = $scope.usergroup;
                    $scope.User.bolIsApprovalLimit = $scope.bolIsApprovalLimit;
                    $scope.User.bolIsNewUser = $scope.bolIsNewUser;
                   /* $scope.User.status = $scope.status;*/
                    $scope.User.bolIsNewBuyer = $scope.bolIsNewBuyer;
                    $scope.User.bolIsManageBuyer = $scope.bolIsManageBuyer;
                    $scope.User.SuperAdmin = $scope.SuperAdmin;
                    $http({
                        method: "post",
                        url: "http://localhost:61646/User/Insert_User",
                        datatype: "json",
                        data: JSON.stringify($scope.User)
                    }).then(function (response) {

                        if (response.data == '2') {

                            $("#UserErrorMessage").text("Username already exist!");
                            $("#btnSave").attr("disabled", false);
                            return;
                            //document.getElementsByClassName('UserErrorMessage').='Username already exist'
                        }
                        if (response.data == 3) {
                            $("#UserErrorMessage").text("Email already exist!");
                            $("#btnSave").attr("disabled", false);
                            return;
                        }
                        else {
                            alert('User added successfully');
                            $scope.GetAllData();
                            $scope.logon_user_id = "";
                            $scope.UserPassword = "";
                            $scope.email = "";
                            $scope.strDepartmentName = "";
                            $scope.bolIsApprovalLimit = "";
                            $scope.bolIsNewUser = "";
                         /*   $scope.bolIsActive = "";*/
                            $("#wait").css("display", "none");
                            $("#UserErrorMessage").text("");
                            $("#btnSave").attr("disabled", false);
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
                    $scope.User = {};
                    $scope.User.intUserCode = $scope.intUserCode;
                    $scope.User.logon_user_id = $scope.logon_user_id;
                    $scope.User.logon_user_name = $scope.logon_user_id;
                    $scope.User.UserPassword = $scope.UserPassword;
                    $scope.User.email = $scope.email;
                    $scope.User.strDepartmentName = $scope.strDepartmentName;
                    $scope.User.bolIsApprovalLimit = $scope.bolIsApprovalLimit;
                    $scope.User.bolIsNewUser = $scope.bolIsNewUser;
                 /*   $scope.User.bolIsActive = $scope.bolIsActive;*/
                    //$scope.User.dtCreatedAt = $scope.dtCreatedAt;
                    //$scope.User.intCreatedByCode = $scope.intCreatedByCode;
                    //$scope.User.dtModifyAt = $scope.dtModifyAt;
                    //$scope.User.intModifyByCode = $scope.intModifyByCode;

                    $http({
                        method: "post",
                        url: "http://localhost:61646/User/Update_User",
                        datatype: "json",
                        data: JSON.stringify($scope.User)
                    }).then(function (response) {

                        if (response.data == '2') {

                            $("#UserErrorMessage").text("Username already exist!");
                            $("#btnSave").attr("disabled", false);
                            return;
                            //document.getElementsByClassName('UserErrorMessage').='Username already exist'
                        }
                        if (response.data == 3) {
                            $("#UserErrorMessage").text("Email already exist!");
                            $("#btnSave").attr("disabled", false);
                            return;
                        }
                        else {
                            alert('User updated successfully');
                            $scope.GetAllData();
                            $scope.logon_user_id = "";
                            $scope.UserPassword = "";
                            $scope.email = "";
                            $scope.strEmail = "";
                            $scope.strDepartmentName = "";
                            $scope.bolIsApprovalLimit = "";
                            $scope.bolIsNewUser = "";
                          /*  $scope.bolIsActive = "";*/
                            window.location.href = "/poapproval/User/UserList"
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
                url: "http://localhost:61646/User/Get_AllUser"
            }).then(function (response) {
                $scope.Users = response.data;
            }, function () {
               
            })
        };
        $scope.DeleteEmp = function (Emp) {
            $http({
                method: "post",
                url: "http://localhost:61646/User/Delete_User",
                datatype: "json",
                data: JSON.stringify(Emp)
            }).then(function (response) {
               // alert(response.data);
                $scope.GetAllData();
            })
        };
        $scope.UpdateUser = function (User) {
            console.log(User);
            //document.getElementById("EmpID_").value = Emp.Emp_Id;
            //$scope.EmpName = Emp.Emp_Name;
            //$scope.EmpCity = Emp.Emp_City;
            //$scope.EmpAge = Emp.Emp_Age;
            //document.getElementById("btnSave").setAttribute("value", "Update");
            //document.getElementById("btnSave").style.backgroundColor = "Yellow";
            //document.getElementById("spn").innerHTML = "Update User Information";

            $scope.intUserCode = User.intUserCode;
            $scope.logon_user_id = User.logon_user_id;
            $scope.UserPassword = User.UserPassword;
            $scope.email = User.email;
            $scope.strDepartmentName = User.strDepartmentName;
            $scope.bolIsApprovalLimit = User.bolIsApprovalLimit;
            $scope.bolIsNewUser = User.bolIsNewUser;
        /*    $scope.bolIsActive = User.bolIsActive;*/
            //$scope.dtCreatedAt = User.dtCreatedAt;
            //$scope.intCreatedByCode = User.intCreatedByCode;
            //$scope.dtModifyAt = User.dtModifyAt;
            //$scope.intModifyByCode = User.intModifyByCode;
            $scope.GetDepartment();
        }
    });