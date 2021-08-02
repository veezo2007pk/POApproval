
var app = angular.module("myApp", ["checklist-model"])
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
        $scope.checkedAll  = function () {
            alert("cehck all");
        }
        $scope.removerequired = function () {

            //alert('ddd');
            //var checkbox_required = $('input[type="checkbox"]');
            ////checkbox_required.prop('required', true);
            //checkbox_required.on('click', function () {
            //    //alert('ddd');
            //    if (checkbox_required.is(':checked')) {
            //        //alert('false');
            //        checkbox_required.prop('required', false);
            //    } else {
            //        //alert('true');
            //        checkbox_required.prop('required', true);
            //    }
            //});
        }
        $scope.getdetails = function () {

            //$scope.User = {};
            //$scope.User.usercode = $scope.usercode;
            //$scope.fullname = "fawad";
            //$scope.bolIsNewBuyer = true;
            //alert($scope.usercode);
            $http({
                method: "POST",
                url: "http://localhost:61646/User/userDetail/",
                dataType: 'json',
                data: { ID: $scope.usercode },
                headers: { "Content-Type": "application/json" }
            }).then(function (data) {
                //console.log(data);
                //console.log(data.data[0].Department);
                $scope.fullname = data.data[0].Username;
                $scope.usergroup = data.data[0].Department;
                $scope.email = data.data[0].Email;
                $scope.pwd = data.data[0].pwd;
                //$scope.bolIsApprovalLimit = data.data[0].bolIsApprovalLimit;
                //$scope.bolIsManageBuyer = data.data[0].bolIsManageBuyer;
             
                //$scope.bolIsNewUser = data.data[0].bolIsNewUser;
                if (data.data[0].SuperAdmin == "1") {
                    $scope.SuperAdmin = true;
                }
                if (data.data[0].UserApprover == "1") {
                    $scope.UserApprover = true;
                }
               
                $scope.xpertLoginID = data.data[0].xpertLoginID;
               
                $("#wait").css("display", "none");
            })
        }
        //debugger;
        $scope.InsertData = function () {
           
            var Action = document.getElementById("btnSave").getAttribute("value");
            if (Action == "Submit") {
                if (confirm('Are you sure you want to insert this?')) {
                  
                    $("#btnSave").attr("disabled", true);
                    $scope.User = {};
                    var arrMembersToNotify = [];
                    var arrMembersToNotifyNew = [];
                    var iCount = 0;
                    $("#membersToNotify input[type=checkbox]:checked").each(function () {
                        //arrMembersToNotify = $(this).val().split(":");
                        //data("foo")
                        arrMembersToNotify = $(this).data("id").split(":");
                        arrMembersToNotifyNew.push({ "menuCode": arrMembersToNotify[1] });
                    });
                    
                    $scope.User.usercode = $scope.usercode;
                    $scope.User.fullname = $scope.fullname;
                    $scope.User.pwd = $scope.pwd;
                    //$scope.User.accesslevels = arrMembersToNotifyNew;
                   
                    $scope.User.email = $scope.email;
                    $scope.User.xpertLoginID = $scope.xpertLoginID;
                    $scope.User.usergroup = $scope.usergroup;
                   /* $scope.User.bolIsApprovalLimit = $scope.bolIsApprovalLimit;*/
                    $scope.User.bolIsNewUser = $scope.bolIsNewUser;
                    $scope.User.status = 1;
                 /*   $scope.User.bolIsNewBuyer = $scope.bolIsNewBuyer;*/
               /*     $scope.User.bolIsManageBuyer = $scope.bolIsManageBuyer;*/
                    $scope.User.SuperAdmin = $scope.SuperAdmin;
                    $scope.User.UserApprover = $scope.UserApprover;
                    //alert(arrMembersToNotifyNew);
                    if (typeof arrMembersToNotifyNew === 'undefined' || arrMembersToNotifyNew === null || arrMembersToNotifyNew == '') {
                        alert("Please select at least 1 user access to proceed");
                        $("#btnSave").attr("disabled", false);
                        return;
                    }
                   
                    $http.post("http://localhost:61646/User/Insert_User", JSON.stringify({
                        data: $scope.User,
                        lstMembersToNotify: arrMembersToNotifyNew
                    })).then(function (response) {
                        //alert(response.data);
                        //if (response.data == '2') {

                        //    $("#UserErrorMessage").text("Username already exist!");
                        //    $("#btnSave").attr("disabled", false);
                        //    return;
                        //    //document.getElementsByClassName('UserErrorMessage').='Username already exist'
                        //}
                        //if (response.data == 3) {
                        //    $("#UserErrorMessage").text("Email already exist!");
                        //    $("#btnSave").attr("disabled", false);
                        //    return;
                        //}
                      /*  if (response.data == '2')  {*/
                            alert('User added successfully');
                            window.location.href = "/poapproval/User/UserList"
                            
                            //$scope.GetAllData();
                            //$scope.logon_user_id = "";
                            //$scope.UserPassword = "";
                            //$scope.email = "";
                            //$scope.strDepartmentName = "";
                            //$scope.bolIsApprovalLimit = "";
                            //$scope.bolIsNewUser = "";
                         /*   $scope.bolIsActive = "";*/
                            $("#wait").css("display", "none");
                            $("#UserErrorMessage").text("");
                            $("#btnSave").attr("disabled", false);
                      /*  }*/
                    }, function (error) {
                            $("#btnSave").attr("disabled", false);
                        alert('Something went wrong, please try again');
                    })
                }
            }
            if (Action == "Update") {
                if (confirm('Are you sure you want to update this?')) {
                    debugger;
                    $("#btnSave").attr("disabled", true);
                    $scope.User = {};
                    var arrMembersToNotify = [];
                    var arrMembersToNotifyNew = [];
                    var iCount = 0;
                    $("#membersToNotify input[type=checkbox]:checked").each(function () {
                        //arrMembersToNotify = $(this).val().split(":");
                        //data("foo")
                        arrMembersToNotify = $(this).data("id").split(":");
                        arrMembersToNotifyNew.push({ "menuCode": arrMembersToNotify[1] });
                    });

                    $scope.User.usercode = $scope.usercode;
                    /*$scope.User.fullname = $scope.fullname;*/
                    $scope.User.pwd = $scope.pwd;
                    /*$scope.User.email = $scope.email;*/
                    $scope.User.xpertLoginID = $scope.xpertLoginID;
                    /*$scope.User.usergroup = $scope.usergroup;*/
                 /*   $scope.User.bolIsApprovalLimit = $scope.bolIsApprovalLimit;*/
                    //$scope.User.bolIsNewUser = $scope.bolIsNewUser;
                    $scope.User.status = 1;
                  /*  $scope.User.bolIsNewBuyer = $scope.bolIsNewBuyer;*/
                   /* $scope.User.bolIsManageBuyer = $scope.bolIsManageBuyer;*/
                    $scope.User.SuperAdmin = $scope.SuperAdmin;
                    $scope.User.UserApprover = $scope.UserApprover;
                 /*   $scope.User.bolIsActive = $scope.bolIsActive;*/
                    //$scope.User.dtCreatedAt = $scope.dtCreatedAt;
                    //$scope.User.intCreatedByCode = $scope.intCreatedByCode;
                    //$scope.User.dtModifyAt = $scope.dtModifyAt;
                    //$scope.User.intModifyByCode = $scope.intModifyByCode;
                    $http.post("http://localhost:61646/User/Update_User", JSON.stringify({
                        data: $scope.User,
                        lstMembersToNotify: arrMembersToNotifyNew
                    })).then(function (response) {

                        if (response.data == '2') {

                            alert("Please select at least 1 user access to proceed");
                            $("#btnSave").attr("disabled", false);
                            return;
                        }
                        //if (response.data == 3) {
                        //    $("#UserErrorMessage").text("Email already exist!");
                        //    $("#btnSave").attr("disabled", false);
                        //    return;
                        //}
                        //else {
                       /* if (response.data == '2') {*/
                            alert('User updated successfully');
                            $scope.GetAllData();
                            $scope.logon_user_id = "";
                            $scope.UserPassword = "";
                            $scope.email = "";
                            $scope.strEmail = "";
                            $scope.strDepartmentName = "";
                            /*$scope.bolIsApprovalLimit = "";*/
                            $scope.bolIsNewUser = "";
                          /*  $scope.bolIsActive = "";*/
                            window.location.href = "/poapproval/User/UserList"
                        /*}*/


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
        $scope.checkAlladd = function () {
            if ($scope.SuperAdmin == true) {


               
                $scope.checkdisable = ""
            } else {

                $scope.checkdisable = "checked"


            }
        };
        $scope.checkAll = function () {
            if ($scope.SuperAdmin == true) {

               
                $scope.userr.roles = [];
                $scope.checkdisable = ""
            } else {
               
                $scope.userr.roles = $scope.roles.map(function (item) { return item.menuCode; });
                $scope.checkdisable = "checked"
               
                
            }
        };
        $scope.DeleteUser = function (Emp) {
            if (confirm('Are you sure you want to delete this?')) {
                alert(Emp);
                //$scope.User = {};
                //$scope.User.usercode = Emp;
                $http({
                    method: "post",
                    url: "http://localhost:61646/User/Delete_User",
                    dataType: 'json',
                    data: { usercode: Emp },
                }).then(function (response) {
                    alert('User Deleted successfully');
                    window.location.href = "/poapproval/User/UserList"
                })
            }
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
        $scope.GetAccessMenus = function () {
            //debugger;
            $http({
                method: "GET",
                url: "http://localhost:61646/User/GetAccessMenus/",
                dataType: 'json',
                data: {},
                headers: { "Content-Type": "application/json" }
            }).then(function (data) {
                
                //$scope.GetAccessMenu = data.data;
                $scope.roles = 
                    data.data
                
                console.log($scope.roles);
            })
        };
        $scope.UpdateUser = function (User) {
            //debugger;
            console.log(User);
            //document.getElementById("EmpID_").value = Emp.Emp_Id;
            //$scope.EmpName = Emp.Emp_Name;
            //$scope.EmpCity = Emp.Emp_City;
            //$scope.EmpAge = Emp.Emp_Age;
            //document.getElementById("btnSave").setAttribute("value", "Update");
            //document.getElementById("btnSave").style.backgroundColor = "Yellow";
            //document.getElementById("spn").innerHTML = "Update User Information";
            
            
            //s=$scope.User.usermenuids.replace("\"", "");
            $scope.usercode = User.usercode;
            $scope.fullname = User.fullname;
            $scope.pwd = User.pwd;
            $scope.email = User.email;
            $scope.usergroup = User.usergroup;
            $scope.usermenuids = User.usermenuids;
            $scope.usermenustrings = User.usermenustring;
            $scope.colors = { Blue: true, Orange: true };
          /*  $scope.bolIsApprovalLimit = User.bolIsApprovalLimit;*/
            //$scope.bolIsNewUser = User.bolIsNewUser;
          /*  $scope.bolIsManageBuyer = User.bolIsManageBuyer;*/
         /*   $scope.bolIsNewBuyer = User.bolIsNewBuyer;*/
            $scope.SuperAdmin = User.SuperAdmin;
            $scope.UserApprover = User.UserApprover;
            $scope.xpertLoginID = User.xpertLoginID;
            //$scope.usermenuids = User.usermenuids;
            //s= $scope.usermenuids.replace("", "");
            $scope.GetAccessMenus();
            $scope.userr = {
                roles: User.usermenuids
            };
            
        /*    $scope.bolIsActive = User.bolIsActive;*/
            //$scope.dtCreatedAt = User.dtCreatedAt;
            //$scope.intCreatedByCode = User.intCreatedByCode;
            //$scope.dtModifyAt = User.dtModifyAt;
            //$scope.intModifyByCode = User.intModifyByCode;
            //$scope.GetDepartment();
        }
    });