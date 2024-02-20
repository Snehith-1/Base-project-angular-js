﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstVisitReportEditController', MstVisitReportEditController);

    MstVisitReportEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];


    function MstVisitReportEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstVisitReportEditController';
       
        var application_gid = $location.search().application_gid;
        var applicationvisit_gid = $location.search().visitreport_gid;
        activate();

        function activate() {
            var url = 'api/MstApplicationVisitReport/DeleteVisittmpDocument';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/MstApplicationVisitReport/DeleteVisittmpPhoto';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/MstApplicationVisitReport/DeleteVisittmpContact';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/MstApplicationVisitReport/DeleteVisittmppersondtl';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/MstApplicationVisitReport/DeleteVisittmpAddress';
            SocketService.get(url).then(function (resp) {

            });
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var params = {
                applicationvisit_gid: applicationvisit_gid
            }
            var url = 'api/MstApplicationVisitReport/EditApplicationVisitReport';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.draft_flag == "Y")
                {
                    $scope.draft = true;
                    $scope.update = false;
                }
                else
                {
                    $scope.draft = false;
                    $scope.update = true;
                }
                $scope.visitreport_id = resp.data.visitreport_id,
                $scope.draft_flag = resp.data.draft_flag,
                $scope.txtdateof_visit = resp.data.applicationvisit_date,
                $scope.txtclient_kmp = resp.data.clientkmp_activities,
                $scope.txtpromoter_background = resp.data.promoter_background;
                $scope.txtoverall_observation = resp.data.overall_observations;
                $scope.txtinspections_official = resp.data.inspectingofficial_recommenation;
                $scope.txttrading_relationship = resp.data.trading_relationship;
                $scope.txtsummary = resp.data.summary;
                $scope.mstinspectingofficials = resp.data.mstinspectingofficials;
                $scope.employeeedit_list = resp.data.employeelist;
                $scope.visitdone_list = resp.data.visitdone_list;
                $scope.msteditvisitlist = resp.data.mdlvisitdone;
                $scope.cboinspecting_list = [];
                if (resp.data.mstinspectingofficials != null) {
                    var count = resp.data.mstinspectingofficials.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.employeeedit_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.mstinspectingofficials[i].employee_gid);
                        $scope.cboinspecting_list.push($scope.employeeedit_list[indexs]);
                    }
                }
                $scope.cbovisit_done = [];
                if (resp.data.visitdone_list != null) {
                    var count = resp.data.visitdone_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.msteditvisitlist.map(function (x) { return x.visitdone_gid; }).indexOf(resp.data.visitdone_list[i].visitdone_gid);
                        $scope.cbovisit_done.push($scope.msteditvisitlist[indexs]);
                    }
                }

            });
            var url = 'api/MstApplicationVisitReport/GetVisitPersondtlList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Visitpersondtl_list = resp.data.mstVisitpersondtl_list;

            });
            var url = 'api/MstApplicationVisitReport/GetVisitAddressList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Visitpersonaddressdtl_list = resp.data.mstVisitpersonaddress_list;

            });

            var url = 'api/MstApplicationVisitReport/GetVisitPhotosList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadphotoList = resp.data.UploadphotoList;

            });

            var url = 'api/MstApplicationVisitReport/GetVisitDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentList = resp.data.UploadDocumentList;

            });
        }

        $scope.Back = function () {
        
            $location.url('app/MstVisitReportAdd?application_gid=' + application_gid);
        }

        $scope.addaddress = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addaddresstype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });
                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }

                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined){
                        $scope.txtlatitude = '';
                        $scope.txtlongitude = '';
                    }
                    else if ($scope.txtpostal_code.length == 6) {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
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

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        addresstype_name: $scope.cboaddresstype.address_type,
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        primary_status: $scope.rdbprimarystatus,
                        address_line1: $scope.txtaddressline1,
                        address_line2: $scope.txtaddressline2,
                        landmark: $scope.txtLand_Mark,
                        postal_code: $scope.txtpostal_code,
                        taluk: $scope.txttaluka,
                        city: $scope.txtcity,
                        state_name: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }

                    var url = 'api/MstApplicationVisitReport/PostVisitAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                    $modalInstance.close('closed');

                }
            }


        }


        $scope.addressdelete = function (val) {
            var params =
                {
                    applicationvisit2address_gid: val
                }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Address ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstApplicationVisitReport/DeleteVisittmpAddressList';
                   
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                        address_list();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });

        }

        function address_list() {
            var params = {
                applicationvisit_gid: applicationvisit_gid
            }
            var url = 'api/MstApplicationVisitReport/GetEditVisitAddressList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Visitpersonaddressdtl_list = resp.data.mstVisitpersonaddress_list;

            });
        }


        $scope.StaticMapAndPhotos_View = function (latitude, longitude, address_line1, address_line2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (address_line2 == '') {
                    var addressString = ''.concat(address_line1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(address_line1.toString(), ",", address_line2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.addvisitedpersondetails = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addvisitpersondetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/Designation/GetDesignation';
                SocketService.get(url).then(function (resp) {
                    $scope.designation_list = resp.data.designation_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.mobileno_add = function () {

                    var params = {
                        mobile_no: $scope.txtmobile_no,
                        whatsapp_mobileno: $scope.rdbwhatsappmobile_no,
                        primary_status: $scope.rdbprimarystatus,
                    }

                    var url = 'api/MstApplicationVisitReport/PostVisitContactNo';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            var url = 'api/MstApplicationVisitReport/GetVisittmpContactList';
                            SocketService.get(url).then(function (resp) {
                                $scope.Visitpersoncontact_list = resp.data.mstVisitpersoncontact_list;
                            });
                            $scope.txtmobile_no = '';
                            $scope.rdbprimarystatus = '';
                            $scope.rdbwhatsappmobile_no = '';
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

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
                $scope.mobileno_delete = function (val) {
                    var params = {
                        applicationvisitperson2contact_gid: val
                    }
                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Contact ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            var url = 'api/MstApplicationVisitReport/DeleteVisittmpContactList';
                            SocketService.getparams(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    var url = 'api/MstApplicationVisitReport/GetVisittmpContactList';
                                    SocketService.get(url).then(function (resp) {
                                        $scope.Visitpersoncontact_list = resp.data.mstVisitpersoncontact_list;
                                    });
                                }
                                else {
                                    Notify.alert('Error Occurred While Deleting Mobile Number!', {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                            });
                            SweetAlert.swal('Deleted Successfully!');
                        }

                    });
                };
                $scope.visitpersondtlSubmit = function () {
                    if ($scope.Visitpersoncontact_list == '' || $scope.Visitpersoncontact_list == undefined || $scope.Visitpersoncontact_list == null) {
                        Notify.alert('Add Atleast One Mobile Number', 'warning');
                    }
                    else 
                    {

                    var params = {
                        clientrepresentative_name: $scope.txtresp_client,
                        clientrepresentative_designationgid: $scope.cboresp_designation.designation_gid,
                        clientrepresentative_designationname: $scope.cboresp_designation.designation_type,
                        clientrepresentative_personalmail: $scope.txtpersonalid_no,
                        clientrepresentative_officemail: $scope.txtofficalid_no,


                    }

                    var url = 'api/MstApplicationVisitReport/PostPersonDetails';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            visitedpersondetails_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                    $modalInstance.close('closed');
                    }

                }


            }
        }

        $scope.visitpersondtldelete = function (val) {
            var params =
                {
                    applicationvisit2person_gid: val
                }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Visit Person Details ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstApplicationVisitReport/DeleteVisittmppersondtlList';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                        visitedpersondetails_list();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }

        function visitedpersondetails_list() {
            var params = {
                applicationvisit_gid: applicationvisit_gid
            }
            var url = 'api/MstApplicationVisitReport/GetEditVisitPersondtlList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Visitpersondtl_list = resp.data.mstVisitpersondtl_list;

            });
        }


        $scope.editaddress = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/editaddresstype.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            var  applicationvisit2address_gid =val
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                var params = {
                    applicationvisit2address_gid: val
                }
                var url = 'api/MstApplicationVisitReport/EditApplicationVisitaddress';
                SocketService.getparams(url, params).then(function (resp) {

                 
                    $scope.cboeditaddresstype = resp.data.addresstype_gid;
                    $scope.rdbeditprimaryaddress = resp.data.primary_status;
                    $scope.txteditaddressline1 = resp.data.address_line1;
                    $scope.txteditaddressline2 = resp.data.address_line2;
                    $scope.txteditLand_Mark = resp.data.landmark;
                    $scope.txteditpostal_code = resp.data.postal_code;
                    $scope.txtedittaluka = resp.data.taluk;
                    $scope.txteditcity = resp.data.city; 
                    $scope.txteditstate = resp.data.state_name;
                    $scope.txtlatitude = resp.data.latitude;
                    $scope.txtlongitude = resp.data.longitude;
                    $scope.txteditdistrict = resp.data.district;
                    $scope.txteditcountry = resp.data.country;
                });
                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txteditpostal_code
                    }

                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txteditcity = resp.data.city;
                        $scope.txtedittaluka = resp.data.taluka;
                        $scope.txteditdistrict = resp.data.district;
                        $scope.txteditstate = resp.data.state_name;
                    });
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined){
                        $scope.txtlatitude = '';
                        $scope.txtlongitude = '';
                    }
                    else if ($scope.txteditpostal_code.length == 6) {
                        if ($scope.txteditaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txteditaddressline1.toString(), ",", $scope.txteditpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txteditaddressline1.toString(), ",", $scope.txteditaddressline2.toString(), ",", $scope.txteditpostal_code.toString());
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

                $scope.txtcountry = "India";

                $scope.addressUpdate = function () {
                    var address_type = $('#address_type :selected').text();

                    var url = 'api/MstApplicationVisitReport/PostPersonaddressUpdate';
                    var params = {
                        applicationvisit2address_gid : applicationvisit2address_gid,
                        addresstype_name: address_type,
                        addresstype_gid: $scope.cboeditaddresstype,
                        primary_status: $scope.rdbeditprimaryaddress,
                        address_line1: $scope.txteditaddressline1,
                        address_line2: $scope.txteditaddressline2,
                        landmark: $scope.txteditLand_Mark,
                        postal_code: $scope.txteditpostal_code,
                        taluk: $scope.txtedittaluka,
                        city: $scope.txteditcity,
                        //state_name: $scope.cboeditstate.state_name,
                        //state_gid: $scope.cboeditstate.state_gid,
                        state_name: $scope.txteditstate,
                        district: $scope.txteditdistrict,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        country: $scope.txteditcountry
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
                        }
                    });

                }
            }
        }


        $scope.saveasdraft_update = function () {
            if ($scope.txtdateof_visit == '' || $scope.txtdateof_visit == null || $scope.txtdateof_visit == undefined) {
                Notify.alert('Select Date Of Visit', 'warning');
            }

            else {
           
            var params = {
                application_gid: application_gid,
                applicationvisit_gid: applicationvisit_gid,
                applicationvisit_date: $scope.txtdateof_visit,
                applicationvisitdate: $scope.txtdateof_visit,
                mstinspectingofficials: $scope.cboinspecting_list,
                mdlvisitdone: $scope.cbovisit_done,
                clientkmp_activities: $scope.txtclient_kmp,
                promoter_background: $scope.txtpromoter_background,
                overall_observations: $scope.txtoverall_observation,
                inspectingofficial_recommenation: $scope.txtinspections_official,
                trading_relationship: $scope.txttrading_relationship,
                summary: $scope.txtsummary,
                statusupdated_by: 'RM',
            }
            var url = 'api/MstApplicationVisitReport/PostSaveApplicationVisitReportUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var params = {
                        application_gid: application_gid,
                        statusupdated_by: 'RM',
                    }
                    var url = 'api/MstApplicationVisitReport/GetVisitReportList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.VisitReportList = resp.data.VisitReportList;

                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/MstVisitReportAdd?application_gid=' + application_gid);
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
    }

        $scope.visitreportSubmit = function () {

           
            var params = {
                application_gid: application_gid,
                applicationvisit_gid: applicationvisit_gid,
                applicationvisit_date: $scope.txtdateof_visit,
                applicationvisitdate: $scope.txtdateof_visit,
                mstinspectingofficials: $scope.cboinspecting_list,
                mdlvisitdone: $scope.cbovisit_done,
                clientkmp_activities: $scope.txtclient_kmp,
                promoter_background: $scope.txtpromoter_background,
                overall_observations: $scope.txtoverall_observation,
                inspectingofficial_recommenation: $scope.txtinspections_official,
                trading_relationship: $scope.txttrading_relationship,
                summary: $scope.txtsummary,
                statusupdated_by: 'RM',
            }
            var url = 'api/MstApplicationVisitReport/PostSubmitApplicationVisitReportUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var params = {
                        application_gid: application_gid,
                        statusupdated_by: 'RM',
                    }
                    var url = 'api/MstApplicationVisitReport/GetVisitReportList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.VisitReportList = resp.data.VisitReportList;

                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/MstVisitReportAdd?application_gid=' + application_gid);
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

        $scope.visitreportUpdate = function () {
         
          
            var params = {
                application_gid: application_gid,
                applicationvisit_gid : applicationvisit_gid,
                applicationvisit_date: $scope.txtdateof_visit,
                applicationvisitdate: $scope.txtdateof_visit,
                mstinspectingofficials: $scope.cboinspecting_list,
                mdlvisitdone: $scope.cbovisit_done,
                clientkmp_activities: $scope.txtclient_kmp,
                promoter_background: $scope.txtpromoter_background,
                overall_observations: $scope.txtoverall_observation,
                inspectingofficial_recommenation: $scope.txtinspections_official,
                trading_relationship: $scope.txttrading_relationship,
                summary: $scope.txtsummary,
                statusupdated_by: 'RM',
            }
            var url = 'api/MstApplicationVisitReport/PostUpdateApplicationVisitReportUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var params = {
                        application_gid: application_gid,
                        statusupdated_by: 'RM',
                    }
                    var url = 'api/MstApplicationVisitReport/GetVisitReportList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.VisitReportList = resp.data.VisitReportList;

                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/MstVisitReportAdd?application_gid=' + application_gid);
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

        $scope.mobileno_summary = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/mobileno_summary.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    applicationvisit2person_gid: val
                }
                var url = 'api/MstApplicationVisitReport/GetVisitContactList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.Visitpersoncontact_list = resp.data.mstVisitpersoncontact_list;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.VisitReportDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i < fi.files.length; i++) {
                    
                    frm.append(fi.files[i].name, fi.files[i]);
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }
                frm.append('document_name', $scope.txtreport_name);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/MstApplicationVisitReport/VisitDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                 
                    unlockUI();
                    if (resp.data.status == true) {
                    var params = {
                        applicationvisit_gid: applicationvisit_gid
                    }
                    var url = 'api/MstApplicationVisitReport/GetEditVisitDocumentList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.UploadDocumentList = resp.data.UploadDocumentList;
                        
                    });
                    
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
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

        $scope.photo_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploaddocumentcancel = function (val) {
          
            var params = {
                applicationvisit2document_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Document ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstApplicationVisitReport/DeleteVisittmpDocumentList';
                    SocketService.getparams(url, params).then(function (resp) {
                     
                        if (resp.data.status == true) {
                            var params = {
                                applicationvisit_gid: applicationvisit_gid
                            }
                            var url = 'api/MstApplicationVisitReport/GetEditVisitDocumentList';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.UploadDocumentList = resp.data.UploadDocumentList;

                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }

        $scope.VisitReportPhotoUpload = function () {
            lockUI();
            var fi = document.getElementById('photo');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i < fi.files.length; i++) {
                    
                    frm.append(fi.files[i].name, fi.files[i]);
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "photoformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }
                frm.append('photo_name', $scope.txtphoto_name);
                frm.append('project_flag', "photo");
                $scope.uploadfrm = frm;
                var url = 'api/MstApplicationVisitReport/VisitPhotoUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#photo").val('');
                    
                    unlockUI();
                    if (resp.data.status == true) {
                    var params = {
                        applicationvisit_gid: applicationvisit_gid
                    }
                    var url = 'api/MstApplicationVisitReport/GetEditVisitPhotosList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.UploadphotoList = resp.data.UploadphotoList;
                    });
                        
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
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

        $scope.uploadphotocancel = function (val) {
          
            var params = {
                applicationvisit2photo_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Photo ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
                }, 
            function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstApplicationVisitReport/DeleteVisittmpPhotoList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
                        if (resp.data.status == true) {
                            var params = {
                                applicationvisit_gid: applicationvisit_gid
                            }
                            var url = 'api/MstApplicationVisitReport/GetEditVisitPhotosList';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.UploadphotoList = resp.data.UploadphotoList;

                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }

        $scope.editvisitedpersondetails = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/editvisitedpersondetails.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            var applicationvisit2person_gid = val;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/Designation/GetDesignation';
                SocketService.get(url).then(function (resp) {
                    $scope.designation_list = resp.data.designation_list;
                });
                var params = {
                    applicationvisit2person_gid: val
                }
                var url = 'api/MstApplicationVisitReport/EditApplicationVisitpersondtl';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditresp_client = resp.data.clientrepresentative_name;
                    $scope.cboeditresp_designation = resp.data.clientrepresentative_designationgid;
                    $scope.txteditpersonalid_no = resp.data.clientrepresentative_personalmail;
                    $scope.txteditofficialid_no = resp.data.clientrepresentative_officemail;

                });
                var url = 'api/MstApplicationVisitReport/GetVisitContactList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.Visitpersoncontact_list = resp.data.mstVisitpersoncontact_list;
                });
                $scope.mobileno_delete = function (val) {
                    var params = {
                        applicationvisitperson2contact_gid: val
                    }
                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Contact ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            var url = 'api/MstApplicationVisitReport/DeleteVisittmpContactList';
                            SocketService.getparams(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    var params = {
                                        applicationvisit2person_gid:applicationvisit2person_gid
                                    }
                                    var url = 'api/MstApplicationVisitReport/GetEditVisitContactList';
                                    SocketService.getparams(url, params).then(function (resp) {
                                        $scope.Visitpersoncontact_list = resp.data.mstVisitpersoncontact_list;
                                    });
                                }
                                else {
                                    Notify.alert('Error Occurred While Deleting Mobile Number!', {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                            });
                            SweetAlert.swal('Deleted Successfully!');
                        }

                    });
                };
                $scope.mobileno_edit = function () {

                    var params = {
                        mobile_no: $scope.txteditmobile_no,
                        whatsapp_mobileno: $scope.rdbeditwhatsappmobile_no,
                        primary_status: $scope.rdbeditprimarystatus,
                    }

                    var url = 'api/MstApplicationVisitReport/PostVisitContactNo';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            var params = {
                                applicationvisit2person_gid: applicationvisit2person_gid
                            }
                            var url = 'api/MstApplicationVisitReport/GetEditVisitContactList';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.Visitpersoncontact_list = resp.data.mstVisitpersoncontact_list;
                            });
                            $scope.txteditmobile_no = '';
                            $scope.rdbeditprimarystatus = '';
                            $scope.rdbeditwhatsappmobile_no = '';
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                          
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
                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.visitpersondtlupdate = function () {
                    var designationtype = $('#designation_type :selected').text();

                    var url = 'api/MstApplicationVisitReport/PostPersonDetailsUpdate';
                    var params = {
                        applicationvisit2person_gid:applicationvisit2person_gid,
                        clientrepresentative_name: $scope.txteditresp_client,
                        clientrepresentative_designationgid: $scope.cboeditresp_designation,
                        clientrepresentative_designationname: designationtype,
                        clientrepresentative_personalmail: $scope.txteditpersonalid_no,
                        clientrepresentative_officemail: $scope.txteditofficialid_no,
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            visitedpersondetails_list();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            visitedpersondetails_list();
                        }
                    });

                }
            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.UploadDocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadDocumentList[i].document_path, $scope.UploadDocumentList[i].filename);
            }
        }

        $scope.downloadallphoto = function () {
            for (var i = 0; i < $scope.UploadphotoList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadphotoList[i].document_path, $scope.UploadphotoList[i].filename);
            }
        }
    }
})();
