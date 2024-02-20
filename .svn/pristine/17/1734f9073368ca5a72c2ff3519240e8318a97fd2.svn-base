(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingIndividualSBAverifyController', MstSAOnboardingIndividualSBAverifyController);

    MstSAOnboardingIndividualSBAverifyController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstSAOnboardingIndividualSBAverifyController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingIndividualSBAverifyController';

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.sacontact_gid = searchObject.lssacontact_gid;
        var sacontact_gid = $scope.sacontact_gid;

        activate();
        function activate() {
            lockUI();
            var url = 'api/MstSAOnboardingIndividual/TempDeleteMobileNo';
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/TempEmailAddress';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingIndividual/TempDocuments';
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/TempProspects';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingIndividual/TempAddress';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingIndividual/TempPanDoc';
            SocketService.get(url).then(function (resp) {
            });

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };


            //lockUI();
            //var url = 'api/MstSAOnboardingInstitution/GetRMName';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
                
            //    $scope.txtRM = resp.data.reporting_manager;
            //});   
            var params = {

                satype_gid: $scope.cbosatype,
                saentitytype_gid: $scope.cbosaentitytype,

                designation_gid: $scope.cboDesignation,
                designation_type: $scope.cboDesignation,
                

                sadocumentlist_gid: $scope.cbosadocument,
                sadocumentlist_name: $scope.cbosadocument
            }
          
            var url = 'api/MstSAOnboardingInstitution/GetDropDown';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadd_salist = resp.data.satype_list;
            });

            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadd_list = resp.data.saentitytype_list;
            });

            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadddoc_list = resp.data.sadocument_list;
            });

            var url = 'api/MstApplicationAdd/PANAbsenceReasonList';
            SocketService.get(url).then(function (resp) {
                $scope.panabsencereason_list = resp.data.panabsencereason_list;
            });
            var param = {
                sacontact_gid: sacontact_gid
            }

            var url = 'api/MstSAOnboardingIndividual/GetSaChequeDocumentEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.sachequedocument_list = resp.data.sachequedocument_list;
            });

            var url = 'api/MstSAOnboardingIndividual/GetMobileNoEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualmobileno_list = resp.data.Sacontactmobileno_list;
            });

            var url = 'api/MstSAOnboardingIndividual/GetEmailAddressEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardemailaddress_list = resp.data.saOnboardemailaddress_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetProspectsEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualprospects_list = resp.data.saOnboardProspects_list; 
            });
            var url = 'api/MstSAOnboardingIndividual/GetAddressEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactindividualaddress_list = resp.data.saOnboardaddress_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetDocumentEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.individualupload_list = resp.data.saOnboardDocument_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetSABureauInstitutionList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.sainstitutebureau_list = resp.data.sainstitutebureau_list;
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetBDIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.bdindividualraisequery_list = resp.data.bdindividualraisequery_list;
            });
            //businessverification
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            //var url = 'api/MstSAOnboardingBussDevtVerification/TagRmLoad';
            //SocketService.get(url).then(function (resp) {
            //    $scope.RM_List = resp.data.Rm_Grp;
            //});
           
            var url = 'api/MstSAOnboardingBussDevtVerification/IndividualDetailsEdit';
    
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtpan_no = resp.data.sa_pannumber;
                if ($scope.txtpan_no != "")
                {
                    $scope.havepan = true;
                    $scope.havenotpan = false;
                    $scope.cbopanstatus = 'Customer Submitting PAN'

                }
                else if ($scope.txtpan_no = "")
                {
                    $scope.havenotpan = true;
                    $scope.havepan = false;
                    $scope.cbopanstatus = 'Customer Submitting Form 60'
                }
                if (resp.data.pan_status != "") {
                    $scope.cbopanstatus = resp.data.pan_status;
                }
                if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                    $scope.havepan = true;
                    $scope.havenotpan = false;
                }
                else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                    $scope.havenotpan = true;
                    $scope.havepan = false;
                }
                var update = resp.data.update_flag;
                $scope.txt_gender = resp.data.gender;
                $scope.satype_name = resp.data.satype_name;
                $scope.saentitytype_name = resp.data.saentitytype_name;
                $scope.txtsafirst_name = resp.data.sa_firstname;
                $scope.txtsamiddle_name = resp.data.sa_middlename;
                $scope.txtsalast_name = resp.data.sa_lastname;
                $scope.txtRM = resp.data.sa_reportingmanager;             
                $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                $scope.txtindividual_aadharnumber = resp.data.sa_aadharnumber;  
                var aadhar = $scope.txtindividual_aadharnumber;
		        var mask=aadhar.slice(-4);
                var maskaadhar='XXXX-XXXX-'+ mask;
                $scope.individual_aadharnumber = maskaadhar;

                
                $scope.txtutr_number = resp.data.sa_apputr;
                $scope.txtcredited_amount = resp.data.sa_appcreditedamount;                
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
                //$scope.txtcrime_check = resp.data.crime_check;
                //$scope.txtbureau_check = resp.data.bureau_check;
                $scope.txtremarks = resp.data.remarks;
                $scope.cbotrainingstatus = resp.data.training_status;
                $scope.cborm = resp.data.rm_tagging_id
                $scope.verify_flag = resp.data.verify_flag;
                $scope.update_flag = resp.data.update_flag;
                $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                $scope.txtassessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                $scope.ratingas_date = resp.data.ratingas_datecredit;
                $scope.lblrcdsource = resp.data.recordsource;
                $scope.txtreferredby = resp.data.referred_by;
                $scope.txtutrno = resp.data.utr_no;
                $scope.present_occupation = resp.data.present_occupation;
                $scope.work_experience = resp.data.work_experience;
                $scope.Expagri_business = resp.data.Expagri_business;
                $scope.lblRM = resp.data.sa_updated_by;

                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/GetPANForm60EditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanform60_list = resp.data.contactpanform60sa_list;
            });
            
            var url = 'api/MstSAOnboardingIndividual/EditPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.panabsencereason_list = resp.data.panabsencereasonsa_list;
                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].check_status == true) {
                        $scope.panabsencereason_list[i].checked = true;
                    }
                }
            });

            var url = 'api/MstSAOnboardingIndividual/ContactPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanabsencereason_list = resp.data.contactpanabsencereasonsa_list;
            });
         
            unlockUI();
            $scope.havenotpan = false;
            $scope.havepan = false;
            $scope.view_nopanreasons = false;


           

        }
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.saindivdualdoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.back = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary');
        }
        $scope.bureau_view = function (saindividual2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/bureau_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var param = {
                    saindividual2bureau_gid: saindividual2bureau_gid
                }

                var url = 'api/MstSAOnboardingIndividual/SABureauView';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureauscore_date = resp.data.bureauscore_date;
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.contact2bureau_gid = resp.data.contact2bureau_gid;

                    $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                    $scope.txtassessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                    $scope.ratingas_date = resp.data.ratingas_date;

                    unlockUI();
                });

                var url = 'api/MstSAOnboardingIndividual/SAUploadIndDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
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
                //$scope.downloads = function (val1, val2) {
                //    var phyPath = val1;
                //    var relPath = phyPath.split("StoryboardAPI");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = location.protocol + "//";
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    link.download = val2;
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                //}

                $scope.bureaudownloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

            }
        }
        $scope.bureau_delete = function (saindividual2bureau_gid) {
            var params = {
                saindividual2bureau_gid: saindividual2bureau_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteContactBureau';
            SocketService.getparams(url, params).then(function (resp) {
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
                    activate();
                }

            });
        }
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
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
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
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


        // PAN Change

        $scope.change_pan = function (cbopanstatus) {
            if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                $scope.havepan = true;
                $scope.havenotpan = false;
                angular.forEach($scope.panabsencereason_list, function (val) {
                    val.checked = false;
                });
                var url = 'api/MstSAOnboardingIndividual/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60sa_list;
                    $scope.contactpanform60_list = '';
                });               
            }
            else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                $scope.havenotpan = true;
                $scope.havepan = false;
                $scope.view_nopanreasons = true;
                $scope.txtpan_no = '';
                $scope.panvalidation = false;
                $scope.txtfirst_name = '';
                $scope.txtmiddle_name = '';
                $scope.txtlast_name = '';
            }
            else {
                $scope.havepan = false;
                $scope.havenotpan = false;
            }
        }

 
        $scope.pandtl_submit = function () {

            var panabsencereason_selectedList = [];
            angular.forEach($scope.panabsencereason_list, function (val) {

                if (val.checked == true) {
                    var panabsencereason = val.panabsencereason;
                    panabsencereason_selectedList.push(panabsencereason);
                }

            });

            var params = {
                panabsencereason_selectedlist: panabsencereason_selectedList,
            }
            var url = 'api/MstSAOnboardingIndividual/PostPANAbsenceReasons';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.view_nopanreasons = false;
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
        $scope.pandtl_close = function () {
            $scope.view_nopanreasons = false;
        }

        $scope.IndividualPANForm60DocumentUpload = function (val, val1, name) {
            lockUI();
            var frm = new FormData();

            for (i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
            }



            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstSAOnboardingIndividual/PANForm60DocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#panform60file").val('');
                        $scope.txtindividualpanform60_document = ''
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        sacontact_gid: sacontact_gid
                    }
                    var url = 'api/MstSAOnboardingIndividual/GetPANForm60TempList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.contactpanform60_list = resp.data.contactpanform60sa_list;
                    });

                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }

        }

        $scope.IndividualPANForm60DocumentDelete = function (sacontact2panform60_gid) {

            var params = {
                sacontact2panform60_gid: sacontact2panform60_gid
            }
            lockUI();
            var url = 'api/MstSAOnboardingIndividual/PANForm60Delete';
            SocketService.getparams(url, params).then(function (resp) {

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
                var param = {
                    sacontact_gid: $scope.sacontact_gid
                }
                var url = 'api/MstSAOnboardingIndividual/GetPANForm60TempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60sa_list;
                });
                unlockUI();
            });
        }


        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbranch_name = resp.data.result.branch;
                        // $scope.txtbank_address = resp.data.result.address;
                        // $scope.txtmicr_code = resp.data.result.micr;

                        if (resp.data.result.micr == "" || resp.data.result.micr == null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbranch_name = '';
                        // $scope.txtbank_address = '';
                        //$scope.txtmicr_code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }
        $scope.bdindividualraisequery = function (sacontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/bdraisequery.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    sacontact_gid: sacontact_gid
                }
                $scope.submit = function () {

                    var params = {
                        sacontact_gid: sacontact_gid,
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_description,

                    }
                    var url = 'api/MstSAOnboardingBussDevtVerification/PostBDIndividualRaiseQuery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //activate();
                            bdraise_list(sacontact_gid);
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }


        function bdraise_list(sacontact_gid) {
            var params = {
                sacontact_gid: sacontact_gid,

            }

            var url = 'api/MstSAOnboardingBussDevtVerification/GetBDIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.bdindividualraisequery_list = resp.data.bdindividualraisequery_list;
            });
        }
        $scope.view_myquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/Raisequery.html',
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

        $scope.BankAccValidation = function () {
            if ($scope.txtbankaccount_number == $scope.txtconfirmbankacct_no) {
                var params = {
                    ifsc: $scope.txtifsc_code,
                    accountNumber: $scope.txtconfirmbankacct_no
                }
                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtaccountholder_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtaccountholder_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        // $scope.onchangeifsc_code = function () {
        //         $scope.txtbank_name='bank name';
        //         $scope.txtbranch_name='branch name';
        //     var params = {
        //         ifsc_code: $scope.txtifsc_code
        //     }
        //   //  var url = 'api/';
        //     SocketService.getparams(url, params).then(function (resp) {
        //         $scope.txtbank_name='bank name';
        //         $scope.txtbranch_name='branch name';
        //     });
        // }
        //Bussverification
        $scope.verify_individual = function () {

            if ($scope.cbotrainingstatus == undefined || $scope.cbotrainingstatus=="") {
                Notify.alert('Please select training status', 'warning')
            }
            else if ($scope.txtremarks == "" || $scope.txtremarks == undefined) {
                Notify.alert('Kindly enter remarks', 'warning')
            }
            else {

                var params = {
                    sacontact_gid: sacontact_gid,
                    bureau_check: $scope.txtbureau_check,
                    crime_check: $scope.txtcrime_check,
                    training_status: $scope.cbotrainingstatus,
                    remarks: $scope.txtremarks,
                    verify_flag: 'Y'
                }
                console.log(params);
                var url = 'api/MstSAOnboardingBussDevtVerification/IndividualVerify';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary');
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
        $scope.verify_reject = function () {
            if ($scope.cbotrainingstatus == undefined || $scope.cbotrainingstatus == "") {
                Notify.alert('Please select Training Status', 'warning')
            }
            else if ($scope.txtremarks == "" || $scope.txtremarks == undefined) {
                Notify.alert('Kindly Enter Remarks', 'warning')
            }
            else {

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
                            sacontact_gid: sacontact_gid,
                            rejected_remarks: $scope.txtreject_remarks,
                            approvalstatus: 'BD Rejected',
                            training_status: $scope.cbotrainingstatus,
                            remarks: $scope.txtremarks
                        }
                        var url = 'api/MstSAOnboardingBussDevtVerification/IndividualRejected';
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

                }
            }
        }
        $scope.individualDocumentUpload = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbocompanydocumentname == null) || ($scope.cbocompanydocumentname == '') || ($scope.cbocompanydocumentname == undefined)) {
                $("#companyfile").val('');
                Notify.alert('Kindly enter the document title/id', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbocompanydocumentname.sadocumentlist_name);
                frm.append('sadocumentlist_gid', $scope.cbocompanydocumentname.sadocumentlist_gid);
                frm.append('document_id', $scope.txtdocument_id);
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstSAOnboardingBussDevtVerification/AddDocuments';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.lsfilename = resp.data.filename;
                        $scope.lsfilepath = resp.data.filepath;        
                        $scope.individualupload_list = resp.data.saOnboardDocument_list;
                        unlockUI();
                        $("#companyfile").val('');
                        $scope.cbocompanydocumentname = "";
                        $scope.txtdocument_id = "";

                        $scope.uploadfrm = undefined;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            var url = 'api/MstSAOnboardingIndividual/GetDocumentsList';
                            SocketService.get(url).then(function (resp) {
                                $scope.lsfilename = resp.data.filename;
                                $scope.lsfilepath = resp.data.filepath;                
                                $scope.individualupload_list = resp.data.saOnboardDocument_list;
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
                        $scope.txtindivual_document = "";
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }

        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.delete_individualdocument = function (sadocument_gid) {
            lockUI();
            var params = {
                sadocument_gid: sadocument_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteDocuments';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var url = 'api/MstSAOnboardingIndividual/GetDocumentsList';
                SocketService.get(url).then(function (resp) {
                    $scope.lsfilename = resp.data.filename;
                    $scope.lsfilepath = resp.data.filepath;                 
                    $scope.individualupload_list = resp.data.saOnboardDocument_list;
                });
                unlockUI();
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

    }
})();