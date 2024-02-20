(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingInstitutionRenewalEditController', MstSAOnboardingInstitutionRenewalEditController);

    MstSAOnboardingInstitutionRenewalEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstSAOnboardingInstitutionRenewalEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingInstitutionRenewalEditController';

        //$scope.sacontactinstitution_gid = $location.search().lssacontactinstitution_gid;
        //var sacontactinstitution_gid = $scope.sacontactinstitution_gid;

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);

        var sacontactinstitution_gid = searchObject.lssacontactinstitution_gid;

        $scope.saentitytype_gid = searchObject.saentitytype_gid;

        var saentitytype_gid = $scope.saentitytype_gid;

        $scope.samfin_code = searchObject.samfin_code;
        var samfin_code = $scope.samfin_code;
        $scope.samagro_code = searchObject.samagro_code;
        var samagro_code = $scope.samagro_code;

        activate();
        function activate() {
            var url = 'api/MstSAOnboardingInstitution/TempDeleteMobileNo';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempEmailAddress';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempAddress';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempProspects';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempGST';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempIndividual';
            SocketService.get(url).then(function (resp) {
            });
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

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
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open4 = true;
            };
            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open5 = true;
            };
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
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
            });
            //var url = 'api/MstSAOnboardingInstitution/DocumentUploadViewList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.lsfilename = resp.data.filename;
            //    $scope.lsfilepath = resp.data.filepath;
            //    $scope.panlist = resp.data.panlist;
            //});
            var url = 'api/MstSAOnboardingInstitution/GetMakerInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.makerinstitutionraisequery_list = resp.data.makerinstitutionraisequery_list;
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetBDInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.bdinstitutionraisequery_list = resp.data.bdinstitutionraisequery_list;
            });
            var url = 'api/MstSAOnboardingInstitution/GetCheckerInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.checkerinstitutionraisequery_list = resp.data.checkerinstitutionraisequery_list;
            });
            var url = 'api/MstSAOnboardingInstitution/GetApproverInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.approverinstitutionraisequery_list = resp.data.approverinstitutionraisequery_list;
            });

            var url = 'api/MstSAOnboardingInstitution/GetSABureauInstitutionList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.sainstitutebureau_list = resp.data.sainstitutebureau_list;
            });
            var url = 'api/MstSAOnboardingInstitution/InstitutionTempClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempBureauDocuments';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/MstSAOnboardingInstitution/SaVerifyDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.instverificationdocument_list = resp.data.sauploaddoc_list;
            });
            var url = 'api/MstSAOnboardingInstitution/ApprovalInitatedDetail';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                //  $scope.cbosatype = resp.data.satype_gid;
                $scope.cbosatype = resp.data.satype_name;
                $scope.cbosaentitytype = resp.data.saentitytype_gid;
                $scope.saentitytype_name = resp.data.saentitytype_name;
                $scope.txtsamunnati_associate_name = resp.data.sa_associatename;
                $scope.txtsacontact_person_first_name = resp.data.sa_contactfirstname;
                $scope.txtsacontact_person_middle_name = resp.data.sa_contactmiddlename;
                $scope.txtsacontact_person_last_name = resp.data.sa_contactlastname;
                // $scope.cboDesignation = resp.data.designation_gid;
                $scope.cboDesignation = resp.data.designation_gid;
                $scope.txtdateofincorporation_date = resp.data.sa_dateofincorporation;
                $scope.txtcompanystart_date = resp.data.sa_companystdate;
                $scope.txtyearin_business = resp.data.sa_yearsinbusiness;
                $scope.txtmonthsin_business = resp.data.sa_monthsinbusiness;
                $scope.txtsa_pannumber = resp.data.sa_companypan;
                $scope.txtannual_turnover = resp.data.sa_annualturnover;              
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
                $scope.txtRM = resp.data.sa_reportingmanager;
                $scope.txtutr_number = resp.data.sa_apputr;
                $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                $scope.txtcredited_amount = resp.data.sa_appcreditedamount;
                $scope.cbotrainingstatus = resp.data.training_status;
                $scope.txtremarks = resp.data.remarks;
                $scope.lblRM = resp.data.sa_updated_by;

                $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                $scope.txtassessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                $scope.ratingas_date = resp.data.ratingas_datecredit;
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
                $scope.bookletnumber = resp.data.bookletnumber;
                $scope.verificationremarks = resp.data.verificationremarks;
                $scope.approvalinitated_flag = resp.data.approvalinitated_flag;
                $scope.approved_date = resp.data.approved_date;
                $scope.approved_by = resp.data.approved_by;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approvalstatus = resp.data.approvalstatus;
                $scope.approval_flag = resp.data.approval_flag;
                $scope.txtutr_number = resp.data.sa_apputr;
                $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                $scope.txtcredited_amount = resp.data.sa_appcreditedamount;
               
                $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                $scope.txtassessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                $scope.ratingas_date = resp.data.ratingas_datecredit;
                $scope.txtreferredby = resp.data.referred_by;
                $scope.lblsamfin = resp.data.samfin_code;
                $scope.lblsamagro = resp.data.samagro_code;
                $scope.lblcodedate = resp.data.codecreation_date;
                $scope.renewal_flag = resp.data.renewal_flag;
                $scope.txtutrno = resp.data.utr_no;

                unlockUI();
            });
            unlockUI();
        }
        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }
        function defaultamountwordschange1(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }
        $scope.companydoc_downloads = function (val1, val2) {
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
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.back = function () {
            $location.url('app/MstSAOnboardingInstitutionRenewalGrouping?hash=' + cmnfunctionService.encryptURL('sacontactinstitution_gid=' + sacontactinstitution_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));

        }
        var params = {

            satype_gid: $scope.cbosatype,
            saentitytype_gid: $scope.cbosaentitytype,

            designation_gid: $scope.cboDesignation,

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

        SocketService.getparams(url, params).then(function (resp) {
            $scope.designationlist = resp.data.sadesignationlist;
        });
        SocketService.get(url).then(function (resp) {
            $scope.saassessmentagencylist = resp.data.saassessmentagencylist;
        });
        SocketService.get(url).then(function (resp) {
            $scope.assessmentagencyratinglist = resp.data.assessmentagencyratinglist;
        });

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
        $scope.bureau_view = function (sainstitution2bureau_gid) {
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
                    sainstitution2bureau_gid: sainstitution2bureau_gid
                }

                var url = 'api/MstSAOnboardingInstitution/SABureauView';

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

                var url = 'api/MstSAOnboardingInstitution/SAUploadIndDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

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
                $scope.downloadallbureau = function (val1, val2) {

                    for (var i = 0; i < val2.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(val1, val2[i]);
                    }

                }
                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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
        $scope.bureau_delete = function (sainstitution2bureau_gid) {
            var params = {
                sainstitution2bureau_gid: sainstitution2bureau_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteContactBureau';
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
       
        $scope.view_campaignquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/makerDescriptionView.html',
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
        $scope.view_checkerquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/checkerDescriptionView.html',
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
        $scope.view_approverquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/approverDescriptionView.html',
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
                        sacontactinstitution_gid: $location.search().lssacontactinstitution_gid
                        //   sacontact_gid: $location.search().lssacontact_gid
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
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.submit_renewal = function () {
            var satypename = $('#satype_name :selected').text();
            var saentitytypename = $('#saentitytype_name :selected').text();
            var designationtype = $('#designation_type :selected').text();
            var empname = $('#employee_name :selected').text();
            var lsassessmentagency_name = $('#assessmentagency_name :selected').text();
            var lsassessmentagencyrating_name = $('#assessmentagencyrating_name :selected').text();

            var params = {
                saentitytype_gid: $scope.cbosaentitytype,
                saentitytype_name: saentitytypename,
                designation_gid: $scope.cboDesignation,
                designation_type: designationtype,
                sa_associatename: $scope.txtsamunnati_associate_name,
                sa_contactfirstname: $scope.txtsacontact_person_first_name,
                sa_contactmiddlename: $scope.txtsacontact_person_middle_name,
                sa_contactlastname: $scope.txtsacontact_person_last_name,
                sa_dateofincorporation: $scope.txtdateofincorporation_date,
                sa_annualturnover: $scope.txtannual_turnover,
                sa_companypan: $scope.txtsa_pannumber,
                sa_companystdate: $scope.txtcompanystart_date,
                sa_yearsinbusiness: $scope.txtyearin_business,
                sa_monthsinbusiness: $scope.txtmonthsin_business,
                sa_apputr: $scope.txtutr_number,
                sa_appcrediteddate: $scope.txtcredited_date,
                sa_appcreditedamount: $scope.txtcredited_amount,
                saifsc_code: $scope.txtifsc_code,
                saaccount_number: $scope.txtbankaccount_number,
                saaccountholder_name: $scope.txtaccountholder_name,
                confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                sacanccheque_number: $scope.txtcancelledcheque_number,
                sabank_name: $scope.txtbank_name,
                sabranch_name: $scope.txtbranch_name,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate,
                micr: $scope.txtmicr,
                branch_address: $scope.txtbranch_address,
                sacontactinstitution_gid: sacontactinstitution_gid,
                //saentitytype_gid: saentitytype_gid,
                //sa_dateofincorporation: $scope.txtdateofincorporation_date,
                //sa_annualturnover: $scope.txtannual_turnover,
                //sa_companypan: $scope.txtsa_pannumber,
                //sa_companystdate: $scope.txtcompanystart_date,
                //sa_yearsinbusiness: $scope.txtyearin_business,
                //sa_monthsinbusiness: $scope.txtmonthsin_business,
                assessmentagency_gid: $scope.cbosaassessmentagency,
                assessmentagency_name: lsassessmentagency_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                assessmentagencyrating_name: lsassessmentagencyrating_name,
                ratingas_date: $scope.txtrasteason_date,
                rdbgstregister_status: $scope.rdbgstregister_status
            }

            var url = 'api/MstSAOnboardingInstitution/InstitutionRenewal';

            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/MstSAOnboardingInstitutionRenewalGrouping?hash=' + cmnfunctionService.encryptURL('sacontactinstitution_gid=' + sacontactinstitution_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));
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
        
        $scope.add_mobileno = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_no == undefined) || ($scope.rdbwhatsapp_no == '') || ($scope.rdbprimary_status == '')) {
                Notify.alert('Enter Mobile No/Select Primary Status and Whatsapp Number', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    samobile_no: $scope.txtmobile_no,
                    saprimary_status: $scope.rdbprimary_status,
                    sacontactinstitution_gid: sacontactinstitution_gid,
                    sawhatsapp_no: $scope.rdbwhatsapp_no
                }
                var url = 'api/MstSAOnboardingInstitution/MobileNumberAddInEdit';
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

                    var params = {
                        sacontactinstitution_gid: sacontactinstitution_gid
                    };
                    var url = 'api/MstSAOnboardingInstitution/GetTempMobileNoList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.SacontactInstimobileno_list = resp.data.SacontactInstimobileno_list;

                    });

                    $scope.txtmobile_no = '';
                    $scope.rdbprimary_status = '';
                    $scope.rdbwhatsapp_no = '';
                    $scope.rdbprimary_no == false;
                });

            }
        }
        $scope.delete_mobileno = function (sainstitution2mobileno_gid) {
            var params =
            {
                sainstitution2mobileno_gid: sainstitution2mobileno_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteMobileNo';

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
            var params =
            {
                sacontactinstitution_gid: sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/GetTempMobileNoList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.SacontactInstimobileno_list = resp.data.SacontactInstimobileno_list;
            });
        }
        //Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbemail_type == undefined) || ($scope.rdbemail_type == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter Email Address/Select Status', 'warning');
            }
            else {
                var params = {
                    saemail_address: $scope.txtemail_address,
                    samail_type: $scope.rdbemail_type,
                    saprimary_status: $scope.rdbprimaryemail_address,
                    sacontactinstitution_gid: sacontactinstitution_gid
                }
                var url = 'api/MstSAOnboardingInstitution/PostEmailAddressInEdit';
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
                    $scope.rdbemail_type = '';
                    $scope.rdbprimaryemail_address == false;
                });
            }
        }
        $scope.delete_emailaddress = function (sainstitution2email_gid) {
            var params =
            {
                sainstitution2email_gid: sainstitution2email_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteEmailAddress';
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
            var params =
            {
                sacontactinstitution_gid: sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/GetEmailAddressTempList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.saOnboardInstiemailaddress_list = resp.data.saOnboardInstiemailaddress_list;
            });
        }

        $scope.add_address = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.onchangebusinessstartdate = function () {
                    var params = {
                        businessstart_date: $scope.txtbusinessstart_date
                    }
                    /* var url = 'api/';*/
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtyear_business = resp.data.year_business;
                        $scope.txtmonth_business = resp.data.month_business;
                    });
                }
                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/MstSAOnboardingInstitution/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.sacity;
                        $scope.txttaluka = resp.data.sataluka;
                        $scope.txtdistrict = resp.data.sadistrict;
                        $scope.txtstate = resp.data.sastate;
                        $scope.txtcountry = resp.data.sacountry
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

                $scope.addressSubmit = function () {
                    var params = {
                        saaddresstype_gid: $scope.cboaddresstype.address_gid,
                        saaddresstype_name: $scope.cboaddresstype.address_type,
                        saprimary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        salandmark: $scope.txtlandmark,
                        sapostal_code: $scope.txtpostal_code,
                        sacity: $scope.txtcity,
                        sataluka: $scope.txttaluka,
                        sadistrict: $scope.txtdistrict,
                        sastate: $scope.txtstate,
                        sacountry: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude

                    }
                    var url = 'api/MstSAOnboardingInstitution/PostAddress';
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
        
        $scope.delete_address = function (sainstitution2address_gid) {
            var params =
            {
                sainstitution2address_gid: sainstitution2address_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteAddress';
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
            var params =
            {
                sacontactinstitution_gid: sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/GetAddressTempList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.saOnboardInstiaddress_list = resp.data.saOnboardInstiaddress_list;
            });
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
        $scope.add_prospects = function () {
            if (($scope.txtlead_name == '') || ($scope.txtlead_name == undefined) || ($scope.txtsector_name == undefined) || ($scope.txtsector_name == '')) {
                Notify.alert('Enter all Mandatory Values', 'warning');
            }
            else {
                var params = {
                    salead_name: $scope.txtlead_name,
                    sacontactinstitution_gid: sacontactinstitution_gid,
                    sasector_industry: $scope.txtsector_name
                }
                var url = 'api/MstSAOnboardingInstitution/AddIndividualProspectsInEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        prospects_list();
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
            $scope.txtlead_name = '',
                $scope.txtsector_name = ''
            prospects_list();

        }
        function prospects_list() {
            var params =
            {
                sacontactinstitution_gid: sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/GetProspectsEditList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.saOnboardInstiProspects_list = resp.data.saOnboardInstiProspects_list;
            });
        }
        $scope.delete_prospects = function (saprospects_institution_gid) {
            var params =
            {
                saprospects_institution_gid: saprospects_institution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteProspects';
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
        $scope.gst_add = function () {


            if (($scope.rdbgstregister_status == undefined) || ($scope.rdbgstregister_status == '') || ($scope.txtgst_state == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_registration_number == undefined) || ($scope.txtgst_registration_number == '')) {


                Notify.alert('Enter GST State / Select GST Registered Status / GST Number', 'warning');
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {

                    gststate_name: $scope.txtgst_state,
                    gst_no: $scope.txtgst_registration_number,
                    gstregister_status: $scope.rdbgstregister_status
                }
                var url = 'api/MstSAOnboardingInstitution/PostInstitutionGST';
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
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    gst_list();
                    $scope.txtgst_state = '';
                    $scope.txtgst_registration_number = '';
                    $scope.rdbgstregister_status = '';

                });
            }
        }

        $scope.gst_delete = function (sainstitution2gst_gid) {
            var params =
            {
                sainstitution2gst_gid: sainstitution2gst_gid
            }
            console.log(params)
            var url = 'api/MstSAOnboardingInstitution/DeleteGST';
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                gst_list();
            });

        }

        function gst_list() {
            var params =
            {
                sacontactinstitution_gid: sacontactinstitution_gid
            };
            var url = 'api/MstSAOnboardingInstitution/InstitutionGSTEditList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.gst_Onboard_list = resp.data.gst_Onboard_list;

            });
        }
        $scope.futuredatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/MstApplicationAdd/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                }
            });
        }


        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtcompanystart_date
            }
            var url = 'api/Mstbuyer/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyearin_business = resp.data.year_business;
                $scope.txtmonthsin_business = resp.data.month_business;
            });
        }

        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_registration_number;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/MstSAOnboardingInstitution/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gststate_name;
            });
        }


        $scope.getPANbasedGST = function () {
            if ($scope.txtsa_pannumber.length == 10) {
                if ($scope.gst_Onboard_list != null) {
                    var paramsdel =
                    {
                        sacontactinstitution_gid: $scope.sacontactinstitution_gid
                    }
                    var url = 'api/MstSAOnboardingInstitution/DeleteGSTInstitution';
                    lockUI();
                    SocketService.getparams(url, paramsdel).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                        }
                        else {
                            Notify.alert('Error occured while deleting the existing GST Details..!', 'warning');
                        }
                    });
                }
                var params = {
                    pan: $scope.txtsa_pannumber
                }
                var url = 'api/Kyc/GSTSBPAN';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/MstSAOnboardingInstitution/PostGST';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                institutiongstlist();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtsa_pannumber
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidation = true;
                                institutiongstlist();
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                                institutiongstlist();
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        function institutiongstlist() {
            var params =
            {
                sacontactinstitution_gid: $scope.sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/InstitutionGSTEditList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.gst_Onboard_list = resp.data.gst_Onboard_list;
            });
        }

        $scope.add_individual = function () {
            if (($scope.txtfirst_name == '') || ($scope.txtfirst_name == undefined) || ($scope.cboindividualdesignation == undefined) || ($scope.txtlast_name == '') || ($scope.txtlast_name == undefined) || ($scope.txtsa_panno == '') || ($scope.txtsa_panno == undefined) || ($scope.txtaadhar_number == '') || ($scope.txtaadhar_number == undefined)) {
                Notify.alert('Enter all Mandatory Values', 'warning');
            }
            else {
                var params = {
                    sa_firstname: $scope.txtfirst_name,
                    sa_middlename: $scope.txtmiddle_name,
                    sa_lastname: $scope.txtlast_name,
                    sa_designation: $scope.cboindividualdesignation,
                    sa_pannumber: $scope.txtsa_panno,
                    sa_aadharnumber: $scope.txtaadhar_number
                }
                var url = 'api/MstSAOnboardingInstitution/PostIndividualDetails'
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        individual_list();
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
            individual_list();
            $scope.txtfirst_name = '',
                $scope.txtmiddle_name = '',
                $scope.txtlast_name = '',
                $scope.cboindividualdesignation = '',
                $scope.txtsa_panno = '',
                $scope.txtaadhar_number = '',
                $scope.panvalidationind = false;
        }
        function individual_list() {
            var params = {
                sacontactinstitution_gid: sacontactinstitution_gid
            };
            var url = 'api/MstSAOnboardingInstitution/GetIndividualList'
            SocketService.getparams(url, params).then(function (resp) {
                $scope.onboard_IndividualInsti_list = resp.data.onboard_IndividualInsti_list;
            });
        }
        $scope.delete_individual = function (sainst_individual_gid) {
            var params =
            {
                sainst_individual_gid: sainst_individual_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteIndividual'
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
                individual_list();
            });
        }
        $scope.PANValidation = function () {
            if ($scope.txtsa_panno.length == 10) {
                var params = {
                    pan: $scope.txtsa_panno
                }
                var url = 'api/Kyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.panvalidationind = true;
                        var parts = resp.data.result.name.split(" ");
                        if (parts.length == 3) {
                            $scope.txtfirst_name = parts[0];
                            $scope.txtmiddle_name = parts[1];
                            $scope.txtlast_name = parts[2];
                        } else {
                            $scope.txtfirst_name = parts[0];
                            $scope.txtlast_name = parts[1];
                        }
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.panvalidationind = false;
                        Notify.alert('PAN is not verified..!', 'warning');
                        $scope.txtfirst_name = '';
                        $scope.txtmiddle_name = '';
                        $scope.txtlast_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }
        $scope.getPANbasedGST = function () {
            if ($scope.txtsa_pannumber.length == 10) {
                if ($scope.gst_Onboard_list != null) {
                    var paramsdel =
                    {
                        sacontactinstitution_gid: $scope.sacontactinstitution_gid
                    }
                    var url = 'api/MstSAOnboardingInstitution/DeleteGSTInstitution';
                    lockUI();
                    SocketService.getparams(url, paramsdel).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                        }
                        else {
                            Notify.alert('Error occured while deleting the existing GST Details..!', 'warning');
                        }
                    });
                }
                var params = {
                    pan: $scope.txtsa_pannumber
                }
                var url = 'api/Kyc/GSTSBPAN';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/MstSAOnboardingInstitution/PostGST';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                institutiongstlist();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtsa_pannumber
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidation = true;
                                institutiongstlist();
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                                institutiongstlist();
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addbureau_institution = function () {
            //if (($scope.cboBureauName == undefined || $scope.cboBureauName == null || $scope.cboBureauName == '') || ($scope.txtbureauscore_date == null || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
            //    ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
            //    ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtobservations == '') || ($scope.txtbureau_response == '' || $scope.txtbureau_response == undefined || $scope.txtbureau_response == null)) {

            //    Notify.alert('Enter All Mandatory Values', 'warning');
            //}
            //else {


            var params = {


                sacontactinstitution_gid: $scope.sacontactinstitution_gid,
                bureauname_gid: $scope.cboBureauName.bureauname_gid,
                bureauname_name: $scope.cboBureauName.bureauname_name,
                bureau_score: $scope.txtbureau_score,
                bureauscore_date: $scope.txtbureauscore_date,
                observations: $scope.txtobservations,
                bureau_response: $scope.txtbureau_response,
            }
            var url = 'api/MstSAOnboardingInstitution/PostSABureauInstitution';
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
                    sacontactinstitution_gid: $scope.sacontactinstitution_gid,
                }
                var url = 'api/MstSAOnboardingInstitution/GetSABureauInstitutionTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sainstitutebureau_list = resp.data.sainstitutebureau_list;

                });



            });
            //}
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
                var url = 'api/MstSAOnboardingInstitution/SaInstitutionDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#bureau_document").val('');
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
                        sainstitution2bureau_gid: $scope.sainstitution2bureau_gid
                    };
                    var url = 'api/MstSAOnboardingInstitution/SAUploadIndividualDocList';
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

        $scope.uploaddocumentcancel = function (institutionsabureaudocumentupload_gid) {
            lockUI();
            var params = {
                institutionsabureaudocumentupload_gid: institutionsabureaudocumentupload_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteBureauDocuments';
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
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                var param = {
                    sainstitution2bureau_gid: $scope.sainstitution2bureau_gid
                };
                var url = 'api/MstSAOnboardingInstitution/SAUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }

        $scope.InstitutionDocumentUpload = function (val, val1, name) {
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
                frm.append('institution_gid', sacontactinstitution_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
            }
        }
        $scope.institutionDoc_upload = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingInstitution/InstitutionUploadDocument';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    var params = {
                        sacontactinstitution_gid: sacontactinstitution_gid

                    }
                    var url = 'api/MstSAOnboardingInstitution/DocumentUploadList';
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


        $scope.deletedocument = function (sainstidocument_gid) {
           
            var params = {
                sainstidocument_gid: sainstidocument_gid
            }
            var url = 'api/MstSAOnboardingInstitution/UploadDocumentDelete';
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
        function docupload_list() {
            var params = {
                sacontactinstitution_gid: sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/UploadDocumentTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.panlist = resp.data.panlist;
            });
        }
        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewerinstitution = function (val1, val2) {
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
                var url = 'api/MstSAOnboardingInstitution/SaInstCancelChequeUpload';
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
            var url = 'api/MstSAOnboardingInstitution/GetSaChequeDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.sachequedocument_list = resp.data.sachequedocument_list;
            });
        }

      
        $scope.credit_amountChange = function () {
            var input = document.getElementById('credit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcredited_amount = "";
            }
            else {
                $scope.txtcredited_amount = output;
                document.getElementById('words_creditamt').innerHTML = lswords_creditamount;
            }
        }
        $scope.annual_turnoverChange = function () {
            var input = document.getElementById('annual_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtannual_turnover = "";
            }
            else {
                $scope.txtannual_turnover = output;
                document.getElementById('words_annualturnover').innerHTML = lswords_annualturnover;
            }
        }
    }
})();