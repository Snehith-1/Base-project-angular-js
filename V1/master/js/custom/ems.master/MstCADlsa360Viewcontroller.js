(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADlsa360Viewcontroller', MstCADlsa360Viewcontroller);

    MstCADlsa360Viewcontroller.$inject =  ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstCADlsa360Viewcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADlsa360Viewcontroller';
        var application_gid = $location.search().application_gid; 
        $scope.lspage = $location.search().lspage;  
        var lsgeneratelsa_gid = $location.search().generatelsa_gid; 
        var application2sanction_gid = $location.search().application2sanction_gid
        var employee_gid = $location.search().employee_gid

        var lspage = $scope.lspage;
        activate();

        function activate() {
            lockUI();
            if (lspage == 'CadLsaApprover')
                $scope.approval_pending = true;
            else
                $scope.approval_pending = false;
            //$scope.lslsfilledlimitproduct = lslsfilledlimitproduct;
            $scope.tobe_recovered = false;
            $scope.already_recovered = false;
            $scope.yes = false;
            $scope.no = false;
            $scope.customer_pnl = true;
            $scope.sanction_pnl = true;
            $scope.signmatching = false;
            $scope.nach_no = false;
            $scope.signmatch_kycprovide = false;
            $scope.escrow_no = false;
            $scope.stamp = false;
            $scope.roc_no = false;
            $scope.cersai_no = false;
            $scope.panel1 = false;
            $scope.panel2 = false;
            $scope.panel3 = false;
            $scope.hidephotodiv = true;
            $scope.showphotodiv = false;

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstCAD/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtentity_gid = resp.data.entity_gid;
                $scope.txtentity_name = resp.data.entity_name;
                $scope.txtcreditapproved_date = resp.data.creditapproved_date;
                $scope.txtvertical_code = resp.data.vertical_code;
            });

            //Sanction Type Drop Down
            var url = 'api/MstCAD/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiontype_list = resp.data.sanctiontype_list;
            });

            var url = 'api/MstCADApplication/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                //$scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbusinessapproved_date = resp.data.businessapproved_date;
                $scope.txtccapproved_date = resp.data.ccapproved_date;
                $scope.txtregion = resp.data.region;
            });

            // Get Credit Approval Hirerichy
            var url = 'api/MstCreditApproval/Getcreditheadsview';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtcredit_head = resp.data.credithead_name;
                $scope.txtnational_manager = resp.data.nationalcredit_name;
                $scope.txtregional_manager = resp.data.regionalcredit_name;
                $scope.txtcredit_manager = resp.data.creditmanager_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.remarks = resp.data.remarks;
            });

            var param = {
                application_gid: application_gid,
                employee_gid: employee_gid,
            }
            // Get RM Approval Hirerichy
            var url = 'api/MstApplicationApproval/Getapplicationhierarchylist';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.clusterhead;
                $scope.zonalhead = resp.data.zonalhead;
                $scope.regionhead = resp.data.regionhead;
                $scope.businesshead = resp.data.businesshead;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCADApplication/GetCadProductChargesDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;
                //$scope.buyer_list = resp.data.mstbuyer_list;
                $scope.txtpurposeof_loan = resp.data.enduse_purpose;
                $scope.txt_processingfee = resp.data.processing_fee;
                $scope.txtprocessing_collecttype = resp.data.processing_collectiontype;
                $scope.txtdoc_charges = resp.data.doc_charges;
                $scope.txtdoc_collecttype = resp.data.doccharge_collectiontype;
                $scope.txtfield_visitcharges = resp.data.fieldvisit_charge;
                $scope.txtfield_collecttype = resp.data.fieldvisit_collectiontype;
                $scope.txtadhoc_fee = resp.data.adhoc_fee;
                $scope.txtadhoccollection_type = resp.data.adhoc_collectiontype;
                $scope.txtlife_insurance = resp.data.life_insurance;
                $scope.txtlife_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.txtaccident_insurance = resp.data.acct_insurance;
                $scope.txttotal_collectible = resp.data.total_collect;
                $scope.txttotal_deductible = resp.data.total_deduct;
                $scope.Collateral_list = resp.data.mstcollateral_list;
                $scope.txtproduct_type = resp.data.product_type;
                $scope.servicecharge_List = resp.data.servicecharge_List;
                $scope.txtsecurity_type = resp.data.security_type;
                $scope.txtsecurity_description = resp.data.security_description;
                $scope.txtsecurity_value = resp.data.security_value;
                $scope.txtsecurityassessed_date = resp.data.securityassessed_date;
                $scope.txtasset_id = resp.data.asset_id;
                $scope.txtroc_fillingid = resp.data.roc_fillingid;
                $scope.txtCERSAI_fillingid = resp.data.CERSAI_fillingid;
                $scope.txthypoobservation_summary = resp.data.hypoobservation_summary;
                $scope.txtprimary_security = resp.data.primary_security;
                $scope.application2hypothecation_gid = resp.data.application2hypothecation_gid;


            });
            var param = {
                application_gid: application_gid
            }
            var url = 'api/MstCADApplication/GetEditLoanDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstloan_list = resp.data.mstloan_list;
                $scope.servicecharges_list = resp.data.servicecharges_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetScheduleMeeting';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmember_list = resp.data.ccmember_list;
                $scope.otheruser_list = resp.data.otheruser_list;
            });

            if (lsgeneratelsa_gid != "" && lsgeneratelsa_gid != undefined) {
                var params = {
                    generatelsa_gid: lsgeneratelsa_gid
                }
                var url = 'api/MstLSA/GetLimitInfoDtl';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.productdetails = resp.data.limitandproducts;
                        $scope.lbltotal_existinglimit = resp.data.total_existinglimit;
                        $scope.lbltotal_limitreleased = resp.data.total_limitreleased;
                        $scope.lblmaker_signature = resp.data.maker_name;
                    }
                });

                var params = {
                    generatelsa_gid: lsgeneratelsa_gid,

                };
                var url = 'api/MstLSA/GetLSAGeneraldocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.filename_list = resp.data.UploadLSADocumentList;
                });

                var url = 'api/MstLSA/GetlsaFeeschargesDetail';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.lsaFeecharges_list = resp.data.lsaFeecharges_list;
                    }
                });
                var url = 'api/MstLSA/Getcompliancecheckinfo';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rdbnachmandateform_held = resp.data.nachmandateform_held,
                  $scope.txtnachmandateform_heldremarks = resp.data.nachmandateform_heldremarks,
                  $scope.rdbsignmatching_nachform = resp.data.signmatching_nachform,
                  $scope.txtsignmatching_nachformremarks = resp.data.signmatching_nachform,
                  $scope.rdbnamesign_kycmatching = resp.data.namesign_kycmatching,
                  $scope.txtnamesign_kycmatchingremarks = resp.data.namesign_kycmatchingremarks,
                  $scope.rdbescrowaccount_opened = resp.data.escrowaccount_opened,
                  $scope.txtescrowaccount_openedremarks = resp.data.escrowaccount_openedremarks,
                  $scope.rdbappropriate_stamping = resp.data.appropriate_stamping,
                  $scope.txtappropriate_stampingremarks = resp.data.appropriate_stampingremarks,
                  $scope.rdbrocfiling_initiated = resp.data.rocfiling_initiated,
                  $scope.txtrocfiling_initiatedremarks = resp.data.rocfiling_initiatedremarks,
                  $scope.rdbcersai_initiated = resp.data.cersai_initiated,
                  $scope.txtcersai_initiatedremarks = resp.data.cersai_initiatedremarks,
                  $scope.rdballdeferralcovenant_captured = resp.data.alldeferralcovenant_captured,
                  $scope.rdballpredisbursement_stipulated = resp.data.allpredisbursement_stipulated,
                  $scope.lblmaker_signature = resp.data.maker_signaturename;
                }); 
            }

            refreshcreditbankaccountsummary();
            refreshbankaccountsummary();
        }

        function refreshservicechargedetails() {
            var params = {
                generatelsa_gid: lsgeneratelsa_gid,

            };
            var url = 'api/MstLSA/GetlsaFeeschargesDetail';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.lsaFeecharges_list = resp.data.lsaFeecharges_list;
                }
            });
        }

        $scope.LSApdf = function () { 
            var params = {
                generatelsa_gid: lsgeneratelsa_gid
            } 
            var url = 'api/MstLSA/GetLSApdf';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                var phyPath = resp.data;
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //link.download = "LSA Report";
                //var uri = str;
                //link.href = uri;
                //link.click();
                //DownloaddocumentService.Downloaddocument(val1, val2);
               

                var phyPath = phyPath.replace("\\", "/");;
                var relPath = phyPath.split("EMS/");
                var relpath1 = relPath[1].replace("\\", "/");
                var url1 = filename1;
                var filename = url1.substring(url1.lastIndexOf('/') + 1);



                var url = 'api/azurestorage/FileUploadDocument';
                var params = {
                    file_path: relpath1
                }
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        DownloaddocumentService.Downloaddocument(relpath1, filename);
                        Notify.alert('LSA Report Downloaded Successfully', 'success')
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export PDF !', 'warning');
                    }
                });
            });

        }

        function refreshproductdetails() {
            if (lsgeneratelsa_gid != "" && lsgeneratelsa_gid != undefined) {
                var params = {
                    generatelsa_gid: lsgeneratelsa_gid
                }
                var url = 'api/MstLSA/GetLimitInfoDtl';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.productdetails = resp.data.limitandproducts;
                        $scope.lbltotal_existinglimit = resp.data.total_existinglimit;
                        $scope.lbltotal_limitreleased = resp.data.total_limitreleased;
                    }
                });
            }
            else {
                lockUI();
                var params = {
                    application_gid: $scope.application_gid,
                    application2sanction_gid: application2sanction_gid
                }
                var url = 'api/MstLSA/GetLSAApplicationLimitInfo';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.productdetails = resp.data.limitandproducts;
                        $scope.lbltotal_existinglimit = resp.data.total_existinglimit;
                        $scope.lbltotal_limitreleased = resp.data.total_limitreleased;
                    }
                });
            }
        }

        $scope.remarks_view = function (limitinfo_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/Remarksproduct.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lbllimitinfo_remarks = limitinfo_remarks;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //$scope.PurposeofLoanOther_view = function (application2loan_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/PurposeOfLoanOther.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        var params =
        //           {
        //               application2loan_gid: application2loan_gid
        //           }
        //        var url = 'api/MstApplicationView/GetPurposeofLoan';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.txtpurposeof_loan = resp.data.enduse_purpose;

        //        });
        //        var url = 'api/MstApplicationView/GetLoanProgramValueChain';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.program = resp.data.program;
        //            $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
        //            $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
        //        });

        //        var url = 'api/MstApplicationView/GetLoanProgramValueChain';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.program = resp.data.program;
        //            $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
        //            $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
        //            $scope.product_gid = resp.data.product_gid;
        //            $scope.product_name = resp.data.product_name;
        //            $scope.variety_gid = resp.data.variety_gid;
        //            $scope.variety_name = resp.data.variety_name;
        //            $scope.sector_name = resp.data.sector_name;
        //            $scope.category_name = resp.data.category_name;
        //            $scope.botanical_name = resp.data.botanical_name;
        //            $scope.alternative_name = resp.data.alternative_name;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //    }

        //}


        function refreshbankaccountsummary() {
            lockUI();
            var params = {
                generatelsa_gid: lsgeneratelsa_gid
            }
            var url = 'api/MstLSA/GetLSABankAccountSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsabankaccsummary_list = resp.data.MdlBankAccount;
            });
        }

        function refreshcreditbankaccountsummary() {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstLSA/GetCreditBankAccountSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditbankacc_list = resp.data.MdlBankAccount;
            });
        }

        function refreshLSAchequeleafDocument(lsabankaccdtl_gid) {
            lockUI();
            var params = {
                lsabankaccdtl_gid: lsabankaccdtl_gid
            }
            var url = 'api/MstLSA/GetLSAchequeleafDocument';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.LSAchequeleafDocument_list = resp.data.lsauploaddocument_list;
            });
        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);

        }
        $scope.view_observationsummary = function (collateralobservation_summary) {
            var modalInstance = $modal.open({
                templateUrl: '/ObservationSummary.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtobservation_summary = collateralobservation_summary;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }

        }

        $scope.viewservicechargesdetails = function (lsachargestype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/DocumentChargesView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //lockUI();
                //var url = "api/MstLSA/GetlsachargesDetail";
                //SocketService.get(url).then(function (resp) {
                //    unlockUI();
                //    $scope.BankNamelist = resp.data.MdlBankName;
                //});

                lockUI();
                var params = {
                    lsachargestype_gid: lsachargestype_gid
                }
                var url = "api/MstLSA/GetlsachargesDetail";
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.chargesdtl = resp.data;
                });

                //$scope.calender8 = function ($event) {
                //    $event.preventDefault();
                //    $event.stopPropagation();
                //    $scope.open8 = true;
                //};

                //$scope.dateOptions = {
                //    formatYear: 'yy',
                //    startingDay: 1
                //};

                //$scope.formats = ['dd-MM-yyyy'];
                //$scope.format = $scope.formats[0];

                $scope.already_recovered = function () {
                    $scope.alreadyrecovered_show = true;
                    $scope.toberecovered_show = false;
                }

                $scope.tobe_recovered = function () {
                    $scope.alreadyrecovered_show = false;
                    $scope.toberecovered_show = true;
                }

                //$scope.recoveredamountchange = function () {
                //    var input = document.getElementById('RecoveredAmount').value;
                //    var str = input.replace(/,/g, '');
                //    var output = Number(str).toLocaleString('en-IN');
                //    var lsrecovered_amt = inWords8(str);
                //    if (output == "NaN") {
                //        Notify.alert('Accept Number Format Only..!', {
                //            status: 'danger',
                //            pos: 'top-center',
                //            timeout: 3000
                //        });
                //        $scope.txtrecovered_amount = "";
                //    }
                //    else {
                //        $scope.txtrecovered_amount = output;
                //        document.getElementById('recoveredamount_words').innerHTML = lsrecovered_amt;
                //    }
                //}

                //function inWords8(num) {
                //    var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
                //    var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
                //    var s = num.toString();
                //    s = s.replace(/[\, ]/g, '');
                //    if (s != parseFloat(s)) return '';
                //    if ((num = num.toString()).length > 9) return 'Overflow';
                //    var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
                //    if (!n) return; var str = '';
                //    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
                //    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
                //    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
                //    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

                //    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
                //    return str;
                //}

                //$scope.recoveredamountchange1 = function () {
                //    var input = document.getElementById('RecoveredAmount1').value;
                //    var str = input.replace(/,/g, '');
                //    var output = Number(str).toLocaleString('en-IN');
                //    var lsrecovered_amount1 = inWords9(str);
                //    if (output == "NaN") {
                //        Notify.alert('Accept Number Format Only..!', {
                //            status: 'danger',
                //            pos: 'top-center',
                //            timeout: 3000
                //        });
                //        $scope.txtrecovered_amount1 = "";
                //    }
                //    else {
                //        $scope.txtrecovered_amount1 = output;
                //        document.getElementById('recoveredamount1_words').innerHTML = lsrecovered_amount1;
                //    }
                //}

                //function inWords9(num) {
                //    var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
                //    var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
                //    var s = num.toString();
                //    s = s.replace(/[\, ]/g, '');
                //    if (s != parseFloat(s)) return '';
                //    if ((num = num.toString()).length > 9) return 'Overflow';
                //    var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
                //    if (!n) return; var str = '';
                //    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
                //    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
                //    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
                //    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

                //    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
                //    return str;
                //}

                //$scope.processingfees_submit = function () {
                //    var params = {
                //        application_gid: application_gid,
                //        recovered_status: $scope.rdbrecovered_type,
                //        recovered_amount: $scope.txtrecovered_amount,
                //        Chequeno_details: $scope.txtprocessingfeescheque_no,
                //        Cheque_date: $scope.txtprocessingfeescheque_date,
                //        processingfees_remarks: $scope.txtalprocessingfees_remarks,
                //        bank_namegid: $scope.cboprocessingfeeschequebank_name,
                //        bank_name: $scope.cboprocessingfeeschequebank_name,
                //    }

                //    lockUI();
                //    var url = 'api/MstLSA/PostProcessingFee';
                //    SocketService.post(url, params).then(function (resp) {
                //        if (resp.data.status == true) {
                //            Notify.alert(resp.data.message, {
                //                status: 'success',
                //                pos: 'top-center',
                //                timeout: 3000
                //            });
                //            unlockUI();
                //            $modalInstance.close('closed');
                //        }
                //        else {
                //            $modalInstance.close('closed');
                //            Notify.alert(resp.data.message, {
                //                status: 'danger',
                //                pos: 'top-center',
                //                timeout: 3000
                //            });
                //            unlockUI();
                //        }
                //    });
                //}

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.view_uploadeddoc = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/DocumentDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/MstLSA/GetLSACollateraldocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.document_list = resp.data.UploadLSADocumentList;
                });
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
                $scope.downloadcollateraldoc = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("StoryboardAPI");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);

                }

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.document_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.document_list[i].document_path, $scope.document_list[i].document_name);
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }

        }

        $scope.Approved_submit = function () {
            var params = {
                generatelsa_gid: lsgeneratelsa_gid, 
            }
            lockUI();
            var url = 'api/MstLSA/PostLSAApproved';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $location.url('app/MstCadLSAApprovalSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        $scope.Back = function () {
            $location.url('app/MstCadLSADtlSummary?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&lspage=' + lspage);
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

        $scope.PurposeofLoan_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoan.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/MstCADApplication/GetCadPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });

                var params =
                {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/MstCADApplication/GetCadLoantoBuyerList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.buyer_list = resp.data.mstbuyer_list;
                });
                var url = 'api/MstCADApplication/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                    $scope.product_gid = resp.data.product_gid;
                    $scope.product_name = resp.data.product_name;
                    $scope.variety_gid = resp.data.variety_gid;
                    $scope.variety_name = resp.data.variety_name;
                    $scope.sector_name = resp.data.sector_name;
                    $scope.category_name = resp.data.category_name;
                    $scope.botanical_name = resp.data.botanical_name;
                    $scope.alternative_name = resp.data.alternative_name;
                    $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        $scope.downloadallfile = function () {
            for (var i = 0; i < $scope.filename_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.filename_list[i].document_path, $scope.filename_list[i].document_name);
            }
        }

        $scope.bankacct_view = function (primarygid, LSABankaccount) {
            var modalInstance = $modal.open({
                templateUrl: '/bankaccount_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var lsabankaccdtl_gid = "", creditbankdtl_gid = "";
                if (LSABankaccount == 'Y') {
                    var params = {
                        lsabankaccdtl_gid: primarygid
                    }
                    var url = 'api/MstLSA/GetLSABankAccountdetail';
                }
                else {
                    var params = {
                        creditbankdtl_gid: primarygid
                    }
                    var url = 'api/MstLSA/GetCreditBankAccountdetail';
                }
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        $scope.viewinfo = resp.data;
                        $scope.uploaddocument_list = resp.data.credituploaddocument_list;
                    }
                    else {
                        unlockUI();
                    }

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloads = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("StoryboardAPI");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
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
                $scope.downloadallche = function () {
                    for (var i = 0; i < $scope.uploaddocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.uploaddocument_list[i].chequeleaf_path, $scope.uploaddocument_list[i].chequeleaf_name);
                    }
                }
            }
        }


        $scope.viewservicechargesdetails = function (lsafeescharge_gid, chargetype) {
            var modalInstance = $modal.open({
                templateUrl: '/DocumentChargesView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                lockUI();
                var params = {
                    lsafeescharge_gid: lsafeescharge_gid,
                    charge_type: chargetype
                }
                var url = "api/MstLSA/GetlsachargesDetail";
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.chargesdtl = resp.data;
                });

             

                $scope.already_recovered = function () {
                    $scope.alreadyrecovered_show = true;
                    $scope.toberecovered_show = false;
                }

                $scope.tobe_recovered = function () {
                    $scope.alreadyrecovered_show = false;
                    $scope.toberecovered_show = true;
                }

               

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

       
    }
})();
