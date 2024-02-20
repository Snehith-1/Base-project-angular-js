(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCcCommitteeKycViewController', AgrMstCcCommitteeKycViewController);

    AgrMstCcCommitteeKycViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll'];

    function AgrMstCcCommitteeKycViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCcCommitteeKycViewController';
        var application_gid = $location.search().application_gid;
        var lsstatus = $location.search().lsstatus;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
        activate();

        function activate() {

            var param = {
                application_gid: application_gid
            }

            var url = 'api/AgrKycView/GetPANAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.panauthentication_list = resp.data.panauthentication_list;
            });

            /*     var url = 'api/KycView/GetPANAadhaarLinkDtl';
                 SocketService.getparams(url, param).then(function (resp) {
                     $scope.panaadhaarlink_list = resp.data.panaadhaarlink_list;
                 }); */

            var url = 'api/AgrKycView/GetDLAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.dlauthentication_list = resp.data.dlauthentication_list;
            });

            var url = 'api/AgrKycView/GetEPICAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.epicauthentication_list = resp.data.epicauthentication_list;
            });

            var url = 'api/AgrKycView/GetIFSCAuthenticationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ifscauthentication_list = resp.data.ifscauthentication_list;
            });

            var url = 'api/AgrKycView/GetBankAccVerificationDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bankaccverification_list = resp.data.bankaccverification_list;
            });

            var url = 'api/AgrKycView/GetGSTSBPANDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.gstsbpan_list = resp.data.gstsbpan_list;
            });

            var url = 'api/AgrMstAPIVerifications/AppnTANList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.tan_list = resp.data.tan_list;
                $scope.tanlist_length = $scope.tan_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnCompanyLLPList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cin_list = resp.data.cin_list;
                $scope.cinlist_length = $scope.cin_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnMCASignatureList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mcasign_list = resp.data.cin_list;
                $scope.mcasignlist_length = $scope.mcasign_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnIECDetailedList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.IECDetailed_list = resp.data.IECDetailed_list;
                $scope.IECDetailedlist_length = $scope.IECDetailed_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnFSSAIList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.fssai_list = resp.data.fssai_list;
                $scope.fssailist_length = $scope.fssai_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnFDAList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.fda_list = resp.data.fda_list;
                $scope.fdalist_length = $scope.fda_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnGSTVerificationList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.gstverification_list = resp.data.gst_list;
                $scope.gstverificationlist_length = $scope.gstverification_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnGSTReturnFilingList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.gstreturnfiling_list = resp.data.gst_list;
                $scope.gstreturnfilinglist_length = $scope.gstreturnfiling_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnGSTAuthenticationList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.gstauthentication_list = resp.data.gst_list;
                $scope.gstauthenticationlist_length = $scope.gstauthentication_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnLPGIDAuthenticationList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.LPGID_list = resp.data.LPGID_list;
                $scope.LPGIDlist_length = $scope.LPGID_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnShopList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.shop_list = resp.data.shop_list;
                $scope.shoplist_length = $scope.shop_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnRCAuthAdvancedList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.RCAuthAdvanced_list = resp.data.RCAuthAdvanced_list;
                $scope.RCAuthAdvancedlist_length = $scope.RCAuthAdvanced_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnRCSearchList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.RCSearch_list = resp.data.RCSearch_list;
                $scope.RCSearchlist_length = $scope.RCSearch_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnPropertyTaxList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.PropertyTax_list = resp.data.PropertyTax_list;
                $scope.PropertyTaxlist_length = $scope.PropertyTax_list.length;
            });
            var url = 'api/AgrProbeAPI/InstitutionProbeLogList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionprobelog_list = resp.data.institutionprobelog_list;
                $scope.institutionprobeloglist_length = $scope.institutionprobelog_list.length;
            });
            var url = 'api/AgrProbeAPI/InstitutionProbeDocLogList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionprobedoclog_list = resp.data.institutionprobedoclog_list;
                $scope.institutionprobedocloglist_length = $scope.institutionprobedoclog_list.length;
            });


        }

        $scope.Back = function () {
            if (lspage == 'ScheduleMeeting') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ScheduleMeeting');
            }
            else if (lspage == 'ScheduledMeetingsummary') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ScheduledMeetingsummary');
            }
            else if (lspage == 'CompletedMeetingsummary') {
                $location.url('app/AgrTrnCCCompletedSummary?application_gid=' + application_gid + '&lspage=CompletedMeetingsummary');
            }
            else if (lspage == 'CCMmeetingScheduledcompleted') {
                $location.url('app/AgrTrnCcCompletedScheduledMeeting?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
            }
            else if (lstab == 'SubmittedToApproval') {
                $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=SubmittedToApproval');
            }
            else if (lstab == 'SubmittedToCC') {
                $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=SubmittedToCC');
            }
            else if (lstab == 'CCSkippedAppl') {
                $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=CCSkippedAppl');
            }
            else if (lstab == 'RejectHoldAppl') {
                $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=RejectHoldAppl');
            }
            else if (lspage == 'SentBackToCredit') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=SentBackToCredit');
            }
            else if (lspage == 'CCMmeetingScheduledcompleted') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
            }
            else if (lstab == 'ProductSubmittedtoApproval') {
                $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=ProductSubmittedtoApproval');
            }
            
            else if (lstab == 'ProductDescRejected') {
                $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=ProductDescRejected');
            }

            else if (lspage == 'ProductDescApproved') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ProductDescApproved');
            }

            else if (lspage == 'ProductDescRejectHold') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ProductDescRejectHold');
            }

            else if (lspage == 'ProductDescAdvance') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ProductDescAdvance');
            }

            else if (lspage == 'ProductDescMyApproval') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ProductDescMyApproval');
            }

            else if (lspage == 'CreditAutoApproval') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CreditAutoApproval');
            }

            else if (lspage == 'AdvanceAC' || lspage == 'UpcomingCreditApproval') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage='+ lspage);
            }
            else if (lstab == 'CCApproved') {
                $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=CCApproved');
            }
            else if (lstab == 'CreditApproval' || lstab == 'CreditApproved' || lstab == 'CreditSubmittedtoCC' || lstab == 'CreditRejectHold') {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab='+ lstab);
            } 
            else {
                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeeting');
            }

        }
        $scope.companyLLPnoView = function (companyllpno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewCompanyandLLPNo.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    companyllpno_gid: companyllpno_gid
                }
                var url = 'api/AgrMstAPIVerifications/CompanyLLPViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcompany_name = resp.data.company_name;
                    $scope.txtroc_code = resp.data.roc_code;
                    $scope.txtregistration_no = resp.data.registration_no;
                    $scope.txtcompany_category = resp.data.company_category;

                    $scope.txtcompany_subcategory = resp.data.company_subcategory;
                    $scope.txtclass_of_company = resp.data.class_of_company;
                    $scope.txtnumber_of_members = resp.data.number_of_members;
                    $scope.txtdate_of_incorporation = resp.data.date_of_incorporation;

                    $scope.txtcompany_status = resp.data.company_status;
                    $scope.txtregistered_address = resp.data.registered_address;
                    $scope.txtalternative_address = resp.data.alternative_address;
                    $scope.txtemail_address = resp.data.email_address;

                    $scope.txtlisted_status = resp.data.listed_status;
                    $scope.txtsuspended_at_stock_exchange = resp.data.suspended_at_stock_exchange;
                    $scope.txtdate_of_last_AGM = resp.data.date_of_last_AGM;
                    $scope.txtdate_of_balance_sheet = resp.data.date_of_balance_sheet;

                    $scope.txtpaid_up_capital = resp.data.paid_up_capital;
                    $scope.txtauthorised_capital = resp.data.authorised_capital;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.mcasignView = function (mcasignatories_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/MCASignatoriesViewDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    mcasignatories_gid: mcasignatories_gid
                }
                var url = 'api/AgrMstAPIVerifications/MCASignatoriesViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.mcasignatorydetails_list = resp.data.mcasignatorydetails_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.IECView = function (iecdtl_gid) {
            var iecdtl_gid = iecdtl_gid;
            localStorage.setItem('iecdtl_gid', iecdtl_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrIECDetailedProfileView";
            window.open(URL, '_blank');
        }
        $scope.FSSAIView = function (fssailicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFSSAIDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    fssailicenseauthentication_gid: fssailicenseauthentication_gid
                }
                var url = 'api/AgrMstAPIVerifications/FSSAIViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtfssai_status = resp.data.fssai_status;
                    $scope.txtlicense_type = resp.data.license_type;
                    $scope.txtlicense_no = resp.data.license_no;
                    $scope.txtfirm_name = resp.data.firm_name;
                    $scope.txtaddress = resp.data.address;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }
        $scope.FDAView = function (fdalicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFDADetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    fdalicenseauthentication_gid: fdalicenseauthentication_gid
                }
                var url = 'api/AgrMstAPIVerifications/FDAViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstore_name = resp.data.store_name;
                    $scope.txtcontact_no = resp.data.contact_no;
                    $scope.txtlicense_detail = resp.data.license_detail;
                    $scope.txtname = resp.data.name;
                    $scope.txtaddress = resp.data.address;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.authenticationView = function (institution2branch_gid) {
            var institution2branch_gid = institution2branch_gid;
            localStorage.setItem('institution2branch_gid', institution2branch_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrGSTAuthenticationView";
            window.open(URL, '_blank');
        }

        $scope.verificationView = function (institution2branch_gid) {
            var institution2branch_gid = institution2branch_gid;
            localStorage.setItem('institution2branch_gid', institution2branch_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrGSPGSTINAuthenticationView";
            window.open(URL, '_blank');
        }

        $scope.returnfillingView = function (institution2branch_gid) {
            var institution2branch_gid = institution2branch_gid;
            localStorage.setItem('institution2branch_gid', institution2branch_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrGSPGSTReturnFilingView";
            window.open(URL, '_blank');
        }

        $scope.LPGIDView = function (lpgiddtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewLPGID.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    lpgiddtl_gid: lpgiddtl_gid
                }
                var url = 'api/AgrMstAPIVerifications/LPGIDViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtApproximateSubsidyAvailed = resp.data.result.ApproximateSubsidyAvailed;
                    $scope.txtSubsidizedRefillConsumed = resp.data.result.SubsidizedRefillConsumed;
                    $scope.txtpin = resp.data.result.pin;

                    $scope.txtConsumerEmail = resp.data.result.ConsumerEmail;
                    $scope.txtDistributorCode = resp.data.result.DistributorCode;
                    $scope.txtBankName = resp.data.result.BankName;
                    $scope.txtIFSCCode = resp.data.result.IFSCCode;

                    $scope.txtAadhaarNo = resp.data.result.AadhaarNo;
                    $scope.txtConsumerContact = resp.data.result.ConsumerContact;
                    $scope.txtDistributorAddress = resp.data.result.DistributorAddress;
                    $scope.txtConsumerName = resp.data.result.ConsumerName;

                    $scope.txtConsumerNo = resp.data.result.ConsumerNo;
                    $scope.txtDistributorName = resp.data.result.DistributorName;
                    $scope.txtBankAccountNo = resp.data.result.BankAccountNo;
                    $scope.txtGivenUpSubsidy = resp.data.result.GivenUpSubsidy;

                    $scope.txtConsumerAddress = resp.data.result.ConsumerAddress;
                    $scope.txtLastBookingDate = resp.data.result.LastBookingDate;
                    $scope.txtTotalRefillConsumed = resp.data.result.TotalRefillConsumed;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.ShopView = function (shopandestablishment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewShopDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    shopandestablishment_gid: shopandestablishment_gid
                }
                var url = 'api/AgrMstAPIVerifications/ShopViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcategory = resp.data.result.category;
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtcommenceDate = resp.data.result.commenceDate;
                    $scope.txttotalWorkers = resp.data.result.totalWorkers;

                    $scope.txtfatherNameOfOccupier = resp.data.result.fatherNameOfOccupier;
                    $scope.txtemail = resp.data.result.email;
                    $scope.txtwebsiteUrl = resp.data.result.websiteUrl;
                    $scope.txtpdfLink = resp.data.result.pdfLink;

                    $scope.txtownerName = resp.data.result.ownerName;
                    $scope.txtaddress = resp.data.result.address;
                    $scope.txtapplicantName = resp.data.result.applicantName;
                    $scope.txtvalidFrom = resp.data.result.validFrom;

                    $scope.txtnatureOfBusiness = resp.data.result.natureOfBusiness;
                    $scope.txtvalidTo = resp.data.result.validTo;
                    $scope.txtregistrationDate = resp.data.result.registrationDate;


                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.RCAuthAdvancedView = function (vehiclercauthadvanced_gid) {
            var vehiclercauthadvanced_gid = vehiclercauthadvanced_gid;
            localStorage.setItem('vehiclercauthadvanced_gid', vehiclercauthadvanced_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrRCAuthAdvancedView";
            window.open(URL, '_blank');
        }

        $scope.RCSearchView = function (vehiclercsearch_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewRCSearchDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    vehiclercsearch_gid: vehiclercsearch_gid
                }
                var url = 'api/AgrMstAPIVerifications/RCSearchViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrc_manu_month_yr = resp.data.result.rc_manu_month_yr;
                    $scope.txtrc_maker_model = resp.data.result.rc_maker_model;
                    $scope.txtrc_f_name = resp.data.result.rc_f_name;
                    $scope.txtrc_eng_no = resp.data.result.rc_eng_no;

                    $scope.txtrc_owner_name = resp.data.result.rc_owner_name;
                    $scope.txtrc_vh_class_desc = resp.data.result.rc_vh_class_desc;
                    $scope.txtrc_present_address = resp.data.result.rc_present_address;
                    $scope.txtrc_color = resp.data.result.rc_color;

                    $scope.txtrc_regn_no = resp.data.result.rc_regn_no;
                    $scope.txttax_paid_upto = resp.data.result.tax_paid_upto;
                    $scope.txtrc_maker_desc = resp.data.result.rc_maker_desc;
                    $scope.txtrc_chasi_no = resp.data.result.rc_chasi_no;

                    $scope.txtrc_mobile_no = resp.data.result.rc_mobile_no;
                    $scope.txtrc_registered_at = resp.data.result.rc_registered_at;
                    $scope.txtrc_valid_upto = resp.data.result.rc_valid_upto;
                    $scope.txtrc_regn_dt = resp.data.result.rc_regn_dt;

                    $scope.txtrc_financer = resp.data.result.rc_financer;
                    $scope.txtrc_permanent_address = resp.data.result.rc_permanent_address;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.PropertyTaxView = function (propertytax_gid) {
            var propertytax_gid = propertytax_gid;
            localStorage.setItem('propertytax_gid', propertytax_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrPropertyTaxView";
            window.open(URL, '_blank');
        }



    }
})();
