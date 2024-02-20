(function () {
    'use strict';

    angular
        .module('angle')
        .controller('samformController', samformController);

    samformController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function samformController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'samformController';
        $scope.sbatype_name = $location.search().satype_name;
        var sbatypename = $scope.satype_name;
        activate();

        function activate() {         
            //$scope.Warning = false;
            //var url = 'api/customer/cMmail';
            //SocketService.get(url).then(function (resp) {
            //    $scope.txtccmail = resp.data.ccmail;
            //});

            //if (sbatype_name = 'Company') {
            //    sbatype_name = 'Company'

            //    // $state.go('app.sbaportalcompany');
            //    $location.url('app/sbaportalcompany?sbatype_name=' + sbatype_name);
            //}
            //else if (sbatype_name = 'Individual') {
            //    //$state.go('app.samforms');
            //    $location.url('app/samforms?sbatype_name=' + sbatype_name);
            //}
          
           

            var params = {

                satype_gid: $scope.cbosatype_name
            }
            var url = 'api/MstSAOnboardingInstitution/GetDropDown';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadd_salist = resp.data.satype_list;
            });
            //console.log($scope.cbosatype_name);
            //$scope.cbosatype_name = $location.search().sbatype_name;

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/AgrMstApplication360/EducationalQualificationList';
            SocketService.get(url).then(function (resp) {
                $scope.educationalqualification_list = resp.data.application_list;
            });

            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });
           
        }

        $scope.onchangepostal_code = function () {
            var params = {
                postal_code: $scope.txtpostalcode
            }
            var url = 'api/MstSAOnboardingIndividual/GetPostalCodeDetails';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtcity = resp.data.city;
                $scope.txttaluka = resp.data.taluka;
                $scope.txtdistrict = resp.data.district;
                $scope.txtstate = resp.data.state;
            });

        }
        $scope.getGeoCoding = function () {
            if ($scope.txtpostalcode.length == 6) {
                if ($scope.txtaddressline2 == undefined) {
                    var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostalcode.toString());
                } else {
                    var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetGeoCoding';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == "OK") {
                        $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                        $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                        $scope.geocodingFailed = false;
                    }
                    else if (resp.data.status == "ZERO_RESULTS") {
                        $scope.geocodingFailed = true;
                    }
                });
            }
        }


        $scope.sbaType_change = function (cbosatype_name) {
           
            if (cbosatype_name.satype_name == 'Company') {
                
                $location.url('app/sbaportalcompany?sbatype_name=' + cbosatype_name.satype_gid);
            }
            else if (cbosatype_name.satype_name == 'Individual')
            {                
                $location.url('app/samforms?sbatype_name=' + cbosatype_name.satype_gid);
            }
           
          //  sbatype_name = 'Company';
        }
      

        $scope.PANValidation = function () {
            if ($scope.txtpannumber.length == 10) {
                var params = {
                    pan: $scope.txtpannumber
                }
                var url = 'api/Kyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.panvalidationind = true;
                        var parts = resp.data.result.name.split(" ");
                        if (parts.length == 3) {
                            $scope.txtfirstname = parts[0];
                            $scope.txtmiddlename = parts[1];
                            $scope.txtlastname = parts[2];
                        } else {
                            $scope.txtfirstname = parts[0];
                            $scope.txtlastname = parts[1];
                        }
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.panvalidationind = false;
                        Notify.alert('PAN is not verified..!', 'warning');
                        $scope.txtfirstname = '';
                        $scope.txtmiddlename = '';
                        $scope.txtlastname = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }
        $scope.sbaform_submit = function () {
            var params = {
                satype_gid: $scope.cbosatype_name.satype_gid,
                satype_name: $scope.cbosatype_name.satype_name,
                pan_number: $scope.txtpannumber,
                aadhar_number: $scope.txtaadhar,
                sbafirst_name: $scope.txtfirstname,
                sbamiddle_name: $scope.txtmiddlename,
                sbalast_name: $scope.txtlastname,
                date_of_birth: $scope.cbodob_date,
                edu_Qualification: $scope.cboEducationalQualification,
                father_name: $scope.txtfathersname,
                present_occupation: $scope.txtpresentoccupation,
                work_experience: $scope.txtworkexperience,
                Expagri_business: $scope.txtexpinagribusiness,
                mobile_no: $scope.txtmobilenumber,
                Alternativemobile_no: $scope.txtaltmobilenumber,
                email_add: $scope.txtpersonalemail,
                address1: $scope.txtaddressline1,
                address2: $scope.txtaddressline2,
                country: 'India',
                state: $scope.txtstate,
                city: $scope.txtcity,
                pincode: $scope.txtpostalcode
               
            }
            var url = 'api/MstSAOnboardingBussDevtVerification/sbiregisterSubmit';

            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000

                    });
                   
                    $scope.txtpannumber = '';
                    $scope.txtaadhar='';
                    $scope.txtfirstname='';
                    $scope.txtmiddlename='';
                    $scope.txtlastname='';
                    $scope.cbodob_date='';
                    $scope.cboEducationalQualification,
                     $scope.txtfathersname='';
                    $scope.txtpresentoccupation='';
                    $scope.txtworkexperience='';
                    $scope.txtexpinagribusiness='';
                     $scope.txtmobilenumber='';
                    $scope.txtaltmobilenumber='';
                     $scope.txtpersonalemail='';
                     $scope.txtaddressline1='';
                     $scope.txtaddressline2='';
                     $scope.cbocountry='';
                    $scope.txtstate='';
                     $scope.txtcity = '';
                     $scope.txtpostalcode = '';
                     $scope.cbosatype_name = '';
                     $scope.pan_document = '';
                     $scope.aaaadhar_document = '';
                     $scope.photo_document=''

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });

        }
        $scope.uploadpan = function (val, val1, name) {
            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

           // frm.append('document_name', $scope.documentname);
            frm.append('document_name', 'Pan');
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingBussDevtVerification/SaIndividualDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    //$scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                   
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }
        $scope.uploadaadhar = function (val, val1, name) {
            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            // frm.append('document_name', $scope.documentname);
            frm.append('document_name', 'Aadhar');
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingBussDevtVerification/SaIndividualDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    //$scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }
        $scope.uploadpassport = function (val, val1, name) {
            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            //frm.append('document_name', $scope.documentname);
            frm.append('document_name', 'Passport');
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingBussDevtVerification/SaIndividualDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    //$scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }
}
})();