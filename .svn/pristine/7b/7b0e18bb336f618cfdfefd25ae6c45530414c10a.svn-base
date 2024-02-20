(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingIndividualRenewalEditController', MstSAOnboardingIndividualRenewalEditController);

    MstSAOnboardingIndividualRenewalEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstSAOnboardingIndividualRenewalEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingIndividualRenewalEditController';


        var searchObject = cmnfunctionService.decryptURL($location.search().hash);

        var sacontact_gid = searchObject.lssacontact_gid;
        $scope.saentitytype_gid = searchObject.saentitytype_gid;

        var saentitytype_gid = $scope.saentitytype_gid;
        $scope.samfin_code = searchObject.samfin_code;
        var samfin_code = $scope.samfin_code;
        $scope.samagro_code = searchObject.samagro_code;
        var samagro_code = $scope.samagro_code;


        /* $scope.renewal_flag = searchObject.renewalflag;*/

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
            var url = 'api/MstSAOnboardingIndividual/TempProspects';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingIndividual/TempAddress';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstSAOnboardingIndividual/TempBureauDocuments';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/MstSAOnboardingIndividual/TempDocuments';
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open4 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open5 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open6 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };



            var params = {
                saentitytype_gid: saentitytype_gid,

            }

            var url = 'api/MstSAOnboardingIndividual/GetEntitytype';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
            });
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

            SocketService.get(url).then(function (resp) {
                $scope.saassessmentagencylist = resp.data.saassessmentagencylist;
            });

            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyratinglist = resp.data.assessmentagencyratinglist;
            });

            //var url = 'api/MstApplicationAdd/PANAbsenceReasonList';
            //SocketService.get(url).then(function (resp) {
            //    $scope.panabsencereason_list = resp.data.panabsencereason_list;
            //});
            var param = {
                sacontact_gid: sacontact_gid
            }

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
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.individualupload_list = resp.data.saOnboardDocument_list;
            });
            var url = 'api/MstSAOnboardingIndividual/DocumentUploadViewList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.renewaldocument_list = resp.data.renewaldocument_list;
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetBDIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.bdindividualraisequery_list = resp.data.bdindividualraisequery_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetMakerIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.makerindividualraisequery_list = resp.data.makerindividualraisequery_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetCheckerIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.checkerindividualraisequery_list = resp.data.checkerindividualraisequery_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetApproverIndividualRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.approverindividualraisequery_list = resp.data.approverindividualraisequery_list;
            });
            var url = 'api/MstSAOnboardingIndividual/SaVerifyDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.instverificationdocument_list = resp.data.sauploaddoc_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetSABureauInstitutionList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactbureau_list = resp.data.sainstitutebureau_list;

            });
            var url = 'api/MstSAOnboardingIndividual/IndividualTempClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });


            var url = 'api/MstSAOnboardingIndividual/ApprovalInitatedDetail';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                //$scope.cbopanstatus = resp.data.pan_status;
                //if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                //    $scope.havepan = true;
                //    $scope.havenotpan = false;
                //}
                //else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                //    $scope.havenotpan = true;
                //    $scope.havepan = false;
                //}
                $scope.cbopanstatus = resp.data.pan_status;
                if (resp.data.sa_pannumber != null || resp.data.sa_pannumber != "") {
                    $scope.cbopanstatus = 'Customer Submitting PAN';
                }
                else {
                    $scope.cbopanstatus = 'Customer Submitting Form 60';
                }
                if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                    $scope.havepan = true;
                    $scope.havenotpan = false;
                }
                else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                    $scope.havenotpan = true;
                    $scope.havepan = false;
                }

                $scope.satype_name = resp.data.satype_name;
                //$scope.saentitytype_name = resp.data.saentitytype_name;
                //$scope.saentitytype_gid = resp.data.saentitytype_gid,
                $scope.cbosaentitytype = resp.data.saentitytype_gid;                        
                    $scope.txtsacontact_person_first_name = resp.data.sa_firstname;
                $scope.txtsacontact_person_middle_name = resp.data.sa_middlename;
                $scope.txtsacontact_person_last_name = resp.data.sa_lastname;
                $scope.txtpan_no = resp.data.sa_pannumber;
                $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                $scope.txtindividual_aadharnumber = resp.data.sa_aadharnumber;
                $scope.cbogender = resp.data.gender;
                $scope.txtutr_number = resp.data.sa_apputr;
                $scope.txtcredited_amount = resp.data.sa_appcreditedamount;  
                $scope.txtifsc_code = resp.data.saifsc_code;
                $scope.txtbankaccount_number = resp.data.saaccount_number;
                $scope.txtconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                $scope.txtaccountholder_name = resp.data.saaccountholder_name;
                $scope.txtcancelledcheque_number = resp.data.sacanccheque_number;
                $scope.txtbranch_name = resp.data.sabranch_name;
                $scope.txtbank_name = resp.data.sabank_name;
                $scope.txtcity = resp.data.city;
                $scope.txtdistrict = resp.data.district;
                $scope.txtstate = resp.data.state;
                $scope.txtmicr = resp.data.micr;
                $scope.txtbranch_address = resp.data.branch_address;
                $scope.txtremarks = resp.data.remarks;
                $scope.cbotrainingstatus = resp.data.training_status;
                $scope.lblRM = resp.data.sa_updated_by;
                $scope.txtRM = resp.data.sa_reportingmanager;

                $scope.cbosaassessmentagency = resp.data.assessmentagency_gid;
                $scope.cboassessmentagencyrating = resp.data.assessmentagencyrating_gid;
                $scope.txtrasteason_date = resp.data.ratingas_datecredit;
                $scope.lblorigination = resp.data.origination;
                $scope.interviewevalution = resp.data.interviewevalution;
                $scope.applicationform = resp.data.applicationform;
                $scope.yearsitreturns = resp.data.yearsitreturns;
                $scope.bankstatement = resp.data.bankstatement;
                $scope.kycdocuments = resp.data.kycdocuments;
                $scope.prospect = resp.data.prospect;
                $scope.vettingstatus = resp.data.vettingstatus;
                $scope.scannedcopyreception = resp.data.scannedcopyreception;
                $scope.addressproof = resp.data.addressproof;
                $scope.photographs = resp.data.photographs;
                $scope.cancelledcheckleaf = resp.data.cancelledcheckleaf;
                $scope.houseofficeverification = resp.data.houseofficeverification;
                $scope.agreementexecutiondate = resp.data.agreementexecutiondate;
                $scope.agreementexpirydate = resp.data.agreementexpirydate;
                $scope.agreementstatus = resp.data.agreementstatus;
                $scope.agroagreementexecutiondate = resp.data.agroagreementexecutiondate;
                $scope.agroagreementexpirydate = resp.data.agroagreementexpirydate;
                $scope.agroagreementstatus = resp.data.agroagreementstatus;
                $scope.verificationremarks = resp.data.verificationremarks;
                $scope.approvalinitated_flag = resp.data.approvalinitated_flag;
                $scope.approval_flag = resp.data.approval_flag;
                $scope.approved_date = resp.data.approved_date;
                $scope.approved_by = resp.data.approved_by;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approvalstatus = resp.data.approvalstatus;
                $scope.lblsamfin = resp.data.samfin_code;
                $scope.lblsamagro = resp.data.samagro_code;
                $scope.lblcodedate = resp.data.codecreation_date;
                $scope.renewal_flag = resp.data.renewal_flag;
                $scope.txtreferredby = resp.data.referred_by;
                $scope.txtutrno = resp.data.utr_no;
                $scope.present_occupation = resp.data.present_occupation;
                $scope.work_experience = resp.data.work_experience;
                $scope.Expagri_business = resp.data.Expagri_business;
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

            var url = 'api/MstSAOnboardingIndividual/GetSABureauIndividualList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.sainstitutebureau_list = resp.data.sainstitutebureau_list;
            });

            unlockUI();
            $scope.havenotpan = false;
            $scope.havepan = false;
            $scope.view_nopanreasons = false;

        }
        $scope.entityname_change = function (cbosaentitytype) {
            var params = {
                saentitytype_gid: $scope.cbosaentitytype,

            }

            var url = 'api/MstSAOnboardingIndividual/GetEntitytype';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
            });

        }

        $scope.saindivdualdoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }

        $scope.downloads = function (val1, val2) {
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
                    $scope.contactpanform60_list = resp.data.contactpanform60_list;
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
                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
            }
        }
        
        $scope.submit_renewal = function () {
            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';
            var saentitytypename = $('#saentitytype_name :selected').text();

            if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
                lsassessmentagency_gid = $scope.cbosaassessmentagency
                lsassessmentagency_name = $('#assessmentagency_name :selected').text()
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating;
                lsassessmentagencyrating_name = $('#assessmentagencyrating_name :selected').text()
            }
            var params = {
                saentitytype_gid: $scope.cbosaentitytype,
                saentitytype_name: saentitytypename,
                sa_firstname: $scope.txtsacontact_person_first_name,
                sa_middlename: $scope.txtsacontact_person_middle_name,
                sa_lastname: $scope.txtsacontact_person_last_name,
                sa_pannumber: $scope.txtpan_no,
                sa_aadharnumber: $scope.txtindividual_aadharnumber,
                sa_apputr: $scope.txtutr_number,
                sa_appcrediteddate: $scope.txtcredited_date,
                sa_appcreditedamount: $scope.txtcredited_amount,
                saifsc_code: $scope.txtifsc_code,
                saaccount_number: $scope.txtbankaccount_number,
                confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                saaccountholder_name: $scope.txtaccountholder_name,
                sacanccheque_number: $scope.txtcancelledcheque_number,
                sabank_name: $scope.txtbank_name,
                sabranch_name: $scope.txtbranch_name,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate, 
                micr: $scope.txtmicr,
                gender: $scope.cbogender,
                branch_address: $scope.txtbranch_address,
                sacontact_gid: sacontact_gid,
                //saentitytype_gid: saentitytype_gid,
                assessmentagency_gid: lsassessmentagency_gid,
                assessmentagency_name: lsassessmentagency_name,
                assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                assessmentagencyrating_name: lsassessmentagencyrating_name,
                ratingas_date: $scope.txtrasteason_date

            }
            console.log(params);
            var url = 'api/MstSAOnboardingIndividual/IndividualRenewal';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/MstSAOnboardingIndividualRenewalGrouping?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
            //}

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
                        $scope.contactpanform60_list = resp.data.contactpanform60_list;
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
                    $scope.contactpanform60_list = resp.data.contactpanform60_list;
                });
                unlockUI();
            });
        }

        $scope.PANValidation = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/MstSAOnboardingBussDevtVerification/IndividualPannumbervalidate';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == false) {
                        Notify.alert(resp.data.message, 'warning')
                        $scope.txtpan_no = '';

                    }
                    else {
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidationind = true;
                                var parts = resp.data.result.name.split(" ");
                                if (parts.length == 3) {
                                    $scope.txtsacontact_person_first_name = parts[0];
                                    $scope.txtsacontact_person_middle_name = parts[1];
                                    $scope.txtsacontact_person_last_name = parts[2];
                                } else {
                                    $scope.txtsacontact_person_first_name = parts[0];
                                    $scope.txtsacontact_person_last_name = parts[1];
                                }
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidationind = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                                $scope.txtsacontact_person_first_name = '';
                                $scope.txtsacontact_person_middle_name = '';
                                $scope.txtsacontact_person_last_name = '';
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });
                    }
                });
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
        $scope.update_individual = function () {
            var panabsencereasons_checked = false;

            for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                if ($scope.panabsencereason_list[i].checked == true) {
                    panabsencereasons_checked = true;
                    break;
                }
            }

            var panabsencereason_selectedList = [];
            angular.forEach($scope.panabsencereason_list, function (val) {
                if (val.checked == true) {
                    var panabsencereason = val.panabsencereason;
                    panabsencereason_selectedList.push(panabsencereason);
                }

            });
            var params = {
                pan_status: $scope.cbopanstatus,
                satype_gid: $scope.cbosatype,
                sa_reportingmanager: $scope.txtRM,
                saentitytype_gid: $scope.cbosaentitytype,
                sacontact_gid: sacontact_gid,
                sa_firstname: $scope.txtsafirst_name,
                sa_middlename: $scope.txtsamiddle_name,
                sa_lastname: $scope.txtsalast_name,
                sa_pannumber: $scope.txtindividual_pannumber,
                sa_aadharnumber: $scope.txtindividual_aadharnumber,
                sa_apputr: $scope.txtutr_number,
                saappcrediteddate: $scope.txtcredited_date,
                sa_appcreditedamount: $scope.txtcredited_amount,
                saifsc_code: $scope.txtifsc_code,
                saaccount_number: $scope.txtbankaccount_number,
                saaccountholder_name: $scope.txtaccountholder_name,
                sacanccheque_number: $scope.txtcancelledchequenumber_name,
                sabank_name: $scope.txtbank_name,
                sabranch_name: $scope.txtbranch_name,
                panabsencereason_selectedlist: panabsencereason_selectedList
            }
            console.log(params);
            var url = 'api/MstSAOnboardingIndividual/IndividualUpdate';
            // lockUI();
            SocketService.post(url, params).then(function (resp) {
                //   unlockUI();
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
            });
            $state.go('app.MstSAOnboardingIndividualSummary');
        }
        $scope.save_individual = function () {
            var params = {
                satype_name: $scope.cbosatype.satype_name,
                satype_gid: $scope.cbosatype.satype_gid,
                saentitytype_name: $scope.cbosaentitytype.saentitytype_name,
                saentitytype_gid: $scope.cbosaentitytype.saentitytype_gid,
                sacontact_person_first_name: $scope.txtsacontact_person_first_name,
                sacontact_person_middle_name: $scope.txtsacontact_person_middle_name,
                sacontact_person_last_name: $scope.txtsacontact_person_last_name,
                individual_pannumber: $scope.txtindividual_pannumber,
                individual_aadharnumber: $scope.txtindividual_aadharnumber,
                sa_apputr: $scope.txtutr_number,
                credited_date: $scope.txtcredited_date,
                credited_amount: $scope.txtcredited_amount,
                ifsc_code: $scope.txtifsc_code,
                bankaccount_number: $scope.txtbankaccount_number,
                accountholder_name: $scope.txtaccountholder_name,
                cancelledchequenumber_name: $scope.txtcancelledchequenumber_name,
                bank_name: $scope.txtbank_name,
                branch_name: $scope.txtbranch_name
            }
            SocketService.post(url, params).then(function (resp) {
                //   unlockUI();
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
            });
            $location.url('app/MstSAOnboardingIndividualRenewalGrouping?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));

        }
        $scope.back = function () {
            $location.url('app/MstSAOnboardingIndividualRenewalGrouping?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));
        }

        $scope.makerindividualquery_close = function (makerindividualraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/makerindividualqueryClose.html',
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
                        makerindividualraisequery_gid: makerindividualraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        sacontact_gid: $location.search().lssacontact_gid
                    }
                    var url = 'api/MstSAOnboardingIndividual/PostMakerIndividualresponsequery';
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
        $scope.view_approverdesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/approverindividualView.html',
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
        $scope.view_makerdesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/makerindividualView.html',
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
        $scope.view_query = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/bdverificationview.html',
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
        $scope.view_checkerdesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/checkerindividualView.html',
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
        $scope.IndividualDocumentUpload = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cboDocumentName == null) || ($scope.cboDocumentName == '') || ($scope.cboDocumentName == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                var frm = new FormData();

                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }
                }

                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cboDocumentName.sadocumentlist_name);
                frm.append('companydocument_gid', $scope.cboDocumentName.sadocumentlist_gid);
                //frm.append('document_id', $scope.txtdocument_id);
                frm.append('individual_gid', sacontact_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
            }
        }
        $scope.individualDoc_upload = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingIndividual/IndividualDocumentUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    var params = {
                        sacontact_gid: sacontact_gid

                    }
                    var url = 'api/MstSAOnboardingIndividual/DocumentUploadList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.renewaldocument_list = resp.data.renewaldocument_list;
                    });

                    unlockUI();

                    $("#document").val('');
                    $scope.cboDocumentName = "";
                    $scope.txtdocument_id = "";
                    $scope.uploadfrm = undefined;

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
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
        }


        $scope.deletedocument = function (sadocument_gid) {
            
            var params = {
                sadocument_gid: sadocument_gid
            }
            var url = 'api/MstSAOnboardingIndividual/UploadDocumentDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                //  $scope.documentupload_list = resp.data.documentupload_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $("#document").val('');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                docupload_list();
              
            });
        }
        function docupload_list() {
            var params = {
                sacontact_gid: sacontact_gid
            }
            var url = 'api/MstSAOnboardingIndividual/UploadDocumentTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.renewaldocument_list = resp.data.renewaldocument_list;
            });
        }
        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewerindividual = function (val1, val2) {
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
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.uploaddownloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.uploaddocumentviewer = function (val1, val2) {
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
        $scope.add_mobileno = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
                Notify.alert('Enter Mobile Number/Select Status', 'warning');
            }
            else {
                var params = {
                    samobile_no: $scope.txtmobile_no,
                    saprimary_status: $scope.rdbprimarymobile_no,
                    sacontact_gid: sacontact_gid,
                    sawhatsapp_no: $scope.rdbwhatsappmobile_no
                }
                var url = 'api/MstSAOnboardingIndividual/MobileNumberAddInEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
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
                    mobileno_list();
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbwhatsappmobile_no = '';
                    $scope.rdbprimarymobile_no == false;
                });
            }
        }
        $scope.delete_mobileno = function (sacontact2mobileno_gid) {
            var params =
            {
                sacontact2mobileno_gid: sacontact2mobileno_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteMobileNo';
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
                mobileno_list();
            });
        }
        function mobileno_list() {
            var param = {
                sacontact_gid: sacontact_gid
            }

            var url = 'api/MstSAOnboardingIndividual/GetMobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualmobileno_list = resp.data.Sacontactmobileno_list;
            });
        }
        //Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter Email Address/Select Status');
            }
            else {
                var params = {
                    saemail_address: $scope.txtemail_address,
                    saprimary_status: $scope.rdbprimaryemail_address,
                    sacontact_gid: sacontact_gid
                }
                var url = 'api/MstSAOnboardingIndividual/PostEmailAddressInEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
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
                    emailaddress_list();
                    $scope.txtemail_address = '';
                    $scope.rdbprimaryemail_address = '';
                    $scope.rdbprimaryemail_address == false;
                });
            }
        }
        $scope.delete_emailaddress = function (sacontact2email_gid) {
            var params =
            {
                sacontact2email_gid: sacontact2email_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteEmailAddress';
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
                emailaddress_list();
            });
        }
        function emailaddress_list() {
            var param = {
                sacontact_gid: sacontact_gid
            }
            var url = 'api/MstSAOnboardingIndividual/GetEmailAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardemailaddress_list = resp.data.saOnboardemailaddress_list;
            });
        }
        //Prospects Multiple Add
        $scope.add_prospects = function () {
            if (($scope.txtlead_name == undefined) || ($scope.txtlead_name == '') || ($scope.txtsector_name == undefined) || ($scope.txtsector_name == '')) {
                Notify.alert('Enter Lead Name/Sector', 'warning');
            }
            else {
                var params = {
                    salead_name: $scope.txtlead_name,
                    sacontact_gid: sacontact_gid,
                    sasector_industry: $scope.txtsector_name
                }

                var url = 'api/MstSAOnboardingIndividual/AddProspectsInEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
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
                    prospects_list();
                });
            }

            $scope.txtlead_name = '',
                $scope.txtsector_name = ''
        }
        function prospects_list() {
            var param = {
                sacontact_gid: sacontact_gid
            }
            var url = 'api/MstSAOnboardingIndividual/GetProspectsTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualprospects_list = resp.data.saOnboardProspects_list;
            });
        }
        $scope.delete_prospects = function (saprospects_gid) {
            var params =
            {
                saprospects_gid: saprospects_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteProspects';
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
                prospects_list();
            });
        }
        //Address Multiple Add
        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addresstype.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });
                var url = 'api/customer/state';
                SocketService.get(url).then(function (resp) {
                    $scope.state_list = resp.data.state_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/MstSAOnboardingIndividual/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state;
                        $scope.txtcountry = resp.data.country;

                    });

                }
                $scope.getGeoCoding = function () {
                    if ($scope.txtpostal_code.length == 6) {
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
                $scope.addressSub = function () {
                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        city: $scope.txtcity,
                        taluka: $scope.txttaluka,
                        district: $scope.txtdistrict,
                        state: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        sacontact_gid: sacontact_gid,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/MstSAOnboardingIndividual/PostAddressInEdit';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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

                    $modalInstance.close('closed');
                }
            }
        }
        $scope.delete_address = function (sacontact2address_gid) {
            var params =
            {
                sacontact2address_gid: sacontact2address_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteAddress';
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
                address_list();
            });
        }
        function address_list() {
            var param = {
                sacontact_gid: sacontact_gid
            }
            var url = 'api/MstSAOnboardingIndividual/GetAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactindividualaddress_list = resp.data.saOnboardaddress_list;
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
        $scope.UploadDocument = function (val, val1, name) {
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstSAOnboardingIndividual/SaInstCancelChequeUpload';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        var url = 'api/Kyc/ChequeOCR';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            if (resp.data.statusCode == 101) {
                                $scope.txtaccountholder_name = resp.data.result.name[0];
                                $scope.txtbankaccount_number = resp.data.result.accNo;
                                $scope.txtconfirmbankacct_no = resp.data.result.accNo;
                                $scope.txtbank_name = resp.data.result.bank;
                                $scope.txtcancelledcheque_number = resp.data.result.chequeNo;
                                $scope.txtifsc_code = resp.data.result.ifsc;
                                $scope.txtmicr = resp.data.result.micr;
                                $scope.txtbranch_address = resp.data.result.bankDetails.address;
                                $scope.txtbranch_name = resp.data.result.bankDetails.branch;
                                $scope.txtcity = resp.data.result.bankDetails.city;
                                $scope.txtdistrict = resp.data.result.bankDetails.district;
                                $scope.txtstate = resp.data.result.bankDetails.state;
                            }
                            else {
                                Notify.alert('Error in fetching values from document..!', 'warning');
                            }
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
                    $("#file").val('');
                    $scope.uploadfrm = undefined;
                    chequedocument_list();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }

        }

        function chequedocument_list() {
            var url = 'api/MstSAOnboardingIndividual/GetSaChequeDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.sachequedocument_list = resp.data.sachequedocument_list;
            });
        }

        $scope.delete_document = function (individualcancelchequeupload_gid) {
            lockUI();
            var params = {
                individualcancelchequeupload_gid: individualcancelchequeupload_gid
            }
            var url = 'api/MstSAOnboardingIndividual/ChequeDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.documentupload_list = resp.data.documentupload_list;
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
                chequedocument_list();
                unlockUI();
            });
        }
        $scope.CICDocumentUpload = function (val, val1, name) {
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingIndividual/SaInstitutionDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#upload_document").val('');
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
                    var param = {
                        saindividual2bureau_gid: $scope.saindividual2bureau_gid
                    };
                    var url = 'api/MstSAOnboardingIndividual/SAUploadIndividualDocList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.lrfilename = resp.data.filename;
                        $scope.lrfilepath = resp.data.filepath;
                        $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }

        $scope.addbureau_individual = function () {
            //if (($scope.cboBureauName == undefined || $scope.cboBureauName == null || $scope.cboBureauName == '') || ($scope.txtbureauscore_date == null || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
            //    ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
            //    ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtobservations == '') || ($scope.txtbureau_response == '' || $scope.txtbureau_response == undefined || $scope.txtbureau_response == null)) {

            //    Notify.alert('Enter All Mandatory Values', 'warning');
            //}
            //else {

            var params = {

                sacontact_gid: sacontact_gid,
                bureauname_gid: $scope.cboBureauName.bureauname_gid,
                bureauname_name: $scope.cboBureauName.bureauname_name,
                bureau_score: $scope.txtbureau_score,
                bureauscore_date: $scope.txtbureauscore_date,
                observations: $scope.txtobservations,
                bureau_response: $scope.txtbureau_response,
            }
            var url = 'api/MstSAOnboardingIndividual/PostSABureauInstitution';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    $scope.cboBureauName = '';
                    $scope.txtbureauscore_date = '';
                    $scope.txtobservations = '';
                    $scope.txtbureau_response = '';
                    $scope.txtbureau_score = '';
                    $scope.cicuploaddoc_list = '';

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {
                    sacontact_gid: sacontact_gid,
                }
                var url = 'api/MstSAOnboardingIndividual/GetSABureauIndividualTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sainstitutebureau_list = resp.data.sainstitutebureau_list;

                });



            });
            //}
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
                $scope.recproof_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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
            }
        }
        $scope.rep_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
       
        //$scope.rep_downloads = function (val1, val2) {
        //    DownloaddocumentService.Downloaddocument(val1, val2);
        //}
    }
})();
