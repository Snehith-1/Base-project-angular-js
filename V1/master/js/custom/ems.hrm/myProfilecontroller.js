(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myProfilecontroller', myProfilecontroller);

    myProfilecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function myProfilecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myProfilecontroller';

        $scope.disablevalue = true;
        $scope.disablepersonaldetails = true;
        $scope.disableaddressdetails = true;

        activate();

        function activate() {

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            var url = "api/myProfile/country";
            SocketService.get(url).then(function (resp) {
                $scope.country_list = resp.data.countryname_list;
            });

            var url = "api/myProfile/employeedetails";
            SocketService.get(url).then(function (resp) {
                console.log(resp);
                $scope.employeedetails = resp.data;
                $scope.txtFirstname = resp.data.first_name;
                $scope.txtLastname = resp.data.last_name;
                $scope.rdgender = resp.data.gender;
                $scope.txtpersonalnum = resp.data.personal_number;
                $scope.dob = resp.data.dob;
                $scope.txtmobile = resp.data.mobile;
                $scope.txtqualification = resp.data.qualification;
                $scope.txtexperience = resp.data.experience;
                $scope.bloodgroup = resp.data.blood_group;

                if (resp.data.employee_photo != "N") {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.employee_photo;
                    str = str.substring(str.indexOf("EMS/") + 3);
                    $scope.employee_photo = url.concat(str);
                }
                else {
                    $scope.employee_photo = resp.data.employee_photo;
                }
                //$scope.dob = Date.parse($scope.dob);
            });

            var url = "api/myProfile/getaddressdetails";
            SocketService.get(url).then(function (resp) {
                $scope.txtaddress1 = resp.data.permanent_address1;
                $scope.txtaddress2 = resp.data.permanent_address2;
                $scope.txtCity = resp.data.permanent_city;
                $scope.txtState = resp.data.permanent_state;
                $scope.txtcountry = resp.data.permanent_country;
                $scope.txtpostalcode = resp.data.permanent_postalcode;
                $scope.tmp_txtaddress1 = resp.data.temporary_address1;
                $scope.tmp_txtaddress2 = resp.data.temporary_address2;
                $scope.tmp_txtCity = resp.data.temporary_city;
                $scope.tmp_txtState = resp.data.temporary_state;
                $scope.tmp_txtcountry = resp.data.temporary_country;
                $scope.tmp_txtpostalcode = resp.data.temporary_postalcode;
            });
        }

        $scope.passwordupdate = function () {
            var params = {
                current_password: $scope.CurrentpassWord,
                new_password: $scope.NewpassWord,
                confirm_passsword: $scope.ConfirmpassWord
            }
            var url = "api/myProfile/updatepassword";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Password Updated Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    $scope.current = $state.current.name;
                    ScopeValueService.store("dataldCtrl", $scope);
                    $state.go('app.pageredirect');
                }
                else {
                    Notify.alert('Error Occured While Updating', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
          
        }

        // Photo Upload //

        $scope.upload = function (val, val1, name) {
           
            var item = {
                name: val[0].name,
                file: val[0]
            };
          
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('employee_gid', $scope.employee_gid);
            $scope.uploadfrm = frm;
            var url = 'api/myProfile/uploadEmployeePhoto';

            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addupload").val('');

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {

                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = "api/myProfile/employeedetails";
                    SocketService.get(url).then(function (resp) {
                        if (resp.data.employee_photo != "N") {
                            var pathArray = location.href.split('/');
                            var protocol = pathArray[0];
                            var host = pathArray[2];
                            var url = protocol + '//' + host;
                            var str = resp.data.employee_photo;
                            str = str.substring(str.indexOf("EMS/") + 3);
                            $scope.employee_photo = url.concat(str);
                        }
                        else {
                            $scope.employee_photo = resp.data.employee_photo;
                        }
                    });
                }
                else {
                    Notify.alert('File Format Not Supported!', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });

        }

        $scope.personalupdate = function () {
            var params = {
                first_name: $scope.txtFirstname,
                last_name: $scope.txtLastname,
                gender: $scope.rdgender,
                dob: $scope.dob,
                mobile: $scope.txtmobile,
                personal_number: $scope.txtpersonalnum,
                qualification: $scope.txtqualification,
                experience: $scope.txtexperience,
                blood_group: $scope.bloodgroup,
                employee_photo: $scope.employeephoto
            }
            var url = "api/myProfile/updateemployeedetails";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Personal Details Updated Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.current = $state.current.name;
                    ScopeValueService.store("dataldCtrl", $scope);
                    $state.go('app.pageredirect');
                }
                else {
                    Notify.alert('Error Occured While Updating', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
              
                //activate();
            });
        }

        $scope.addressupdate = function () {
            var params = {
                permanent_address1: $scope.txtaddress1,
                permanent_address2: $scope.txtaddress2,
                permanent_city: $scope.txtCity,
                permanent_state: $scope.txtState,
                permanent_country: $scope.txtcountry,
                permanent_postalcode: $scope.txtpostalcode,
                temporary_address1: $scope.tmp_txtaddress1,
                temporary_address2: $scope.tmp_txtaddress2,
                temporary_city: $scope.tmp_txtCity,
                temporary_state: $scope.tmp_txtState,
                temporary_country: $scope.tmp_txtcountry,
                temporary_postalcode: $scope.tmp_txtpostalcode
            }
            var url = "api/myProfile/updateaddressdetails";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Address Details Updated Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.current = $state.current.name;
                    ScopeValueService.store("dataldCtrl", $scope);
                    $state.go('app.pageredirect');
                }
                else {
                    Notify.alert('Error Occured While Updating', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
               // activate();
            });
        }

        $scope.changeedit = function () {
            $scope.disablevalue = false;
            $scope.changeUpdate = true;
            $scope.changeCancel = true;
            $scope.changeedit = false;
           
        }

        $scope.chcancel = function () {
            $scope.current = $state.current.name;
            ScopeValueService.store("dataldCtrl", $scope);
            $state.go('app.pageredirect');
            //$scope.changeedit = true;
            //$scope.changeUpdate = false;
            //$scope.changeCancel = false;
            //$scope.disablevalue = true;
        }


        $scope.personaledit = function () {
            $scope.disablepersonaldetails = false;
            $scope.personalUpdate = true;
            $scope.personalCancel = true;
            $scope.personaledit = false;
        }

        $scope.personalcancel = function () {
            $scope.current = $state.current.name;
            ScopeValueService.store("dataldCtrl", $scope);
            $state.go('app.pageredirect');
        }

        $scope.addressedit = function () {
            $scope.disableaddressdetails = false;
            $scope.addressUpdate = true;
            $scope.addressCancel = true;
            $scope.addressedit = false;
        }

        $scope.addresscancel = function () {
            $scope.current = $state.current.name;
            ScopeValueService.store("dataldCtrl", $scope);
            $state.go('app.pageredirect');
        }


    }
})();



