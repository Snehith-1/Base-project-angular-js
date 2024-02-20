(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAonboardingSBAinstitutionverificationController', MstSAonboardingSBAinstitutionverificationController);

    MstSAonboardingSBAinstitutionverificationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstSAonboardingSBAinstitutionverificationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
    /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAonboardingSBAinstitutionverificationController';
        
      
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.sacontactinstitution_gid = searchObject.lssacontactinstitution_gid;
        var sacontactinstitution_gid = $scope.sacontactinstitution_gid;

        activate();
        function activate() {  
        
            var param = {
            sacontactinstitution_gid: sacontactinstitution_gid
            }

            var url = 'api/MstSAOnboardingInstitution/InstitutionGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                 $scope.gst_Onboard_list = resp.data.gst_Onboard_list;
             });
             var url = 'api/MstSAOnboardingInstitution/InstitutionMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.SacontactInstimobileno_list = resp.data.SacontactInstimobileno_list;
             });
             var url = 'api/MstSAOnboardingInstitution/InstitutionEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardInstiemailaddress_list = resp.data.saOnboardInstiemailaddress_list;
             });
             var url = 'api/MstSAOnboardingInstitution/InstitutionAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardInstiaddress_list = resp.data.saOnboardInstiaddress_list;
             });
             var url = 'api/MstSAOnboardingInstitution/IndividualList';
             SocketService.getparams(url, param).then(function (resp) {
                $scope.onboard_IndividualInsti_list = resp.data.onboard_IndividualInsti_list;
              });
              var url = 'api/MstSAOnboardingInstitution/GetInstitutionProspectsList';
              SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardInstiProspects_list = resp.data.saOnboardInstiProspects_list;
               });
               var url = 'api/MstSAOnboardingInstitution/UploadList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lrfilename = resp.data.filename;
                $scope.lrfilepath = resp.data.filepath;
                $scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
                });
               var url = 'api/MstSAOnboardingInstitution/GetMakerInstitutionRaiseQuery';
               lockUI();
               SocketService.getparams(url, param).then(function (resp) {
                   unlockUI();
                   $scope.makerinstitutionraisequery_list = resp.data.makerinstitutionraisequery_list;
               });
               var url = 'api/MstSAOnboardingInstitution/GetCheckerInstitutionRaiseQuery';
               lockUI();
               SocketService.getparams(url, param).then(function (resp) {
                   unlockUI();
                   $scope.checkerinstitutionraisequery_list = resp.data.checkerinstitutionraisequery_list;
               });

            //businessverification
               var url = 'api/MstSAOnboardingBussDevtVerification/GetEmployeelist';
               SocketService.get(url).then(function (resp) {
                   $scope.employee_list1 = resp.data.rmemployeelists;
               });

               var url = 'api/MstSAOnboardingBussDevtVerification/InstitutionDetailsEdit';
    
               lockUI();
               SocketService.getparams(url, param).then(function (resp) {
                   $scope.lblrecdsce = resp.data.recordsource;
                   $scope.cbosatype = resp.data.satype_gid;
                   $scope.satype_name = resp.data.satype_name;
                   $scope.cbosaentitytype = resp.data.saentitytype_gid;       
                   $scope.saentitytype_name = resp.data.saentitytype_name;   
                   $scope.txtsamunnati_associate_name = resp.data.sa_associatename;
                   $scope.txtsacontact_person_first_name = resp.data.sa_contactfirstname;
                   $scope.txtsacontact_person_middle_name = resp.data.sa_contactmiddlename;
                   $scope.txtsacontact_person_last_name = resp.data.sa_contactlastname;
                   $scope.cboDesignation = resp.data.designation_gid;
                   $scope.designation_type = resp.data.designation_type;
                   $scope.txtdateofincorporation_date = resp.data.sa_dateofincorporation;
                   $scope.txtcompanystart_date = resp.data.sa_companystdate;           
                   $scope.txtyearin_business = resp.data.sa_yearsinbusiness;
                   $scope.txtmonthsin_business = resp.data.sa_monthsinbusiness;
                   $scope.txtsa_pannumber = resp.data.sa_companypan;
                   $scope.txtannual_turnover = resp.data.sa_annualturnover;
                   $scope.lblifsc_code = resp.data.saifsc_code;
                   $scope.lblbankaccount_number = resp.data.saaccount_number;
                   $scope.lblconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                   $scope.lblaccountholder_name = resp.data.saaccountholder_name;
                   $scope.lblcancelledcheque_number = resp.data.sacanccheque_number;
                   $scope.lblbranch_name = resp.data.sabranch_name;
                   $scope.lblbank_name = resp.data.sabank_name;
                   $scope.lblmicr = resp.data.micr;
                   $scope.lblcity = resp.data.city;
                   $scope.lblstate = resp.data.state;
                   $scope.lbldistrict = resp.data.district;
                   $scope.lblbranchaddress = resp.data.branch_address;
                   $scope.txtRM = resp.data.sa_reportingmanager;
                   $scope.txtutr_number = resp.data.sa_apputr;
                   $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                   $scope.txtcredited_amount = resp.data.sa_appcreditedamount;
                   //$scope.cboemployee = resp.data.rm_tagging_id;
                   $scope.txtreferredby = resp.data.referred_by;
                   $scope.txtutrno = resp.data.utr_no;


                   
                   unlockUI();
               });
           unlockUI();
        }



        $scope.downloadallbureau = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.documentbureauviewer = function (val1, val2) {
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
        $scope.companydoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.back = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationSummary');
        }
        $scope.update_institution_RM = function () {
            if ($scope.txtremarks == undefined || $scope.txtremarks == null) {
                Notify.alert('Please enter remarks', 'warning')
            }
            else {

            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';

            if ($scope.cboemployee != undefined || $scope.cboemployee != null) {
                lsassessmentagency_gid = $scope.cboemployee
                lsassessmentagency_name = $('#RM :selected').text()
            }
                var params = {
                    sacontactinstitution_gid: sacontactinstitution_gid,
                    rm_tagging_id: $scope.cboemployee,
                    rm_tagging_name: lsassessmentagency_name,
                    tagged_remarks: $scope.txtremarks
                }
                console.log(params);
                var url = 'api/MstSAOnboardingBussDevtVerification/InstitutionRMUpdate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.MstSAOnboardingBussDevtVerificationSummary');
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

        $scope.reject_institution_RM = function () {
            //if ($scope.cboemployee == undefined || $scope.cboemployee == "") {
            //    Notify.alert('Please select Training Status', 'warning')
            //}
            //else if ($scope.txtremarks == "" || $scope.txtremarks == undefined) {
            //    Notify.alert('Kindly Enter Remarks', 'warning')
            //}
            //else {

                var modalInstance = $modal.open({
                    templateUrl: '/rejectrequest.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.ok = function () {
                        modalInstance.close('closed');
                    };
                    $scope.rejectSubmit = function () {
                        var params = {
                            sacontactinstitution_gid: sacontactinstitution_gid,
                            rejected_remarks: $scope.txtreject_remarks,                           
                            training_status: $scope.cbotrainingstatus,
                            remarks: $scope.txtremarks
                        }
                        var url = 'api/MstSAOnboardingBussDevtVerification/InstitutionRegisterationRejected';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                modalInstance.close('closed');
                                $state.go("app.MstSAOnboardingBussDevtVerificationSummary");
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                modalInstance.close('closed');
                            }
                        });
                        $state.go("app.MstSAOnboardingBussDevtVerificationSummary");
                    }
                //}
            }
        }


        $scope.mycampaignquery_close = function (makerinstitutionraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaignqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        makerinstitutionraisequery_gid: makerinstitutionraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        sacontactinstitution_gid: $location.search().lssacontactinstitution_gid
                    }
                    var url = 'api/MstSAOnboardingInstitution/PostMakerInstitutionresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
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
        $scope.view_campaignquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaignqueryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.checkerinstitutionquery_close = function (checkerinstitutionraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/checkerinstitutionqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        checkerinstitutionraisequery_gid: checkerinstitutionraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        sacontact_gid: $location.search().lssacontact_gid
                    }
                    var url = 'api/MstSAOnboardingInstitution/PostCheckerInstitutionresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
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
        $scope.view_checkerquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/checkerinstitutionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }



        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, sapostal_code) {
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
                    longitude: longitude,
                    latitude: latitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", sapostal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", sapostal_code.toString());
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
        $scope.documentviewerupload = function (val1, val2) {
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


       
    }
})();