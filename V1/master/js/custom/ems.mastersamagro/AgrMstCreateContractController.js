(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCreateContractController', AgrMstCreateContractController);

    AgrMstCreateContractController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstCreateContractController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCreateContractController';
        const lsdynamiclimitmanagementback = 'AgrMstCreateContract';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        //$scope.sanction_gid = $location.search().sanction_gid;
        //var sanction_gid = $location.search().sanction_gid;
        $scope.lsapplication2sanction_gid = $location.search().lsapplication2sanction_gid;
        var lsapplication2sanction_gid = $location.search().lsapplication2sanction_gid;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.lsresubmit = $location.search().lsresubmit;
        var froalaConfigKey = apiManage.GetCommonData['froalaConfig'].Key;
        var lsresubmit = $scope.lsresubmit;
        if (localStorage.getItem('RefreshTemplate') && localStorage.getItem('RefreshTemplate') != 'Y') {
            location.reload();
            localStorage.setItem('RefreshTemplate', 'Y');
            return false;
        }
        var lspage = $scope.lspage;
        var vertical_gid;
        var vertical_code;
        lockUI();
        activate();
       
        function activate() {
            var params = {
                application_gid: $scope.application_gid,
                tmp_status: false
            }
            var url = 'api/AgrMstApplicationAdd/GetApplicationTradeList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MdlTradelist = resp.data.MdlTradedtl;
                if ($scope.MdlTradelist == null) {
                    $scope.Tradedivshow = true;
                }
                else {
                    $scope.Tradedivshow = false;
                    $scope.TradeEditdivshow = false;
                }
                unlockUI();
            });

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            $scope.showedittemplate = false;
            $scope.showpreviewtemplate = false;
            $scope.showdefaulttemplate = true;

            var url = window.location.href;
            $scope.warningfacility_amount = true;
            $scope.existing_customer = false;
            $scope.amount_validation = true;
            $scope.facility_pnl = false;
            $scope.addfacility_pnl = true;
            $scope.panel = true;
            $scope.panel1 = true;
            $scope.warningfacility_amount = true;
            $scope.amount_validation = true;
            $scope.interchangeabilityno = false;
            $scope.interchangeabilityyes = false;
            $scope.onchangefacility = false;
            $scope.sanction_validation = false;
            $scope.warningcbomember = true;
            $scope.warningcbofacility = true;
            $scope.mandatoryfields = false;
            $scope.validitymonth = false;
            vm.calenderEdit = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.openEdit = true;
            };
            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open6 = true;
            };
            vm.calender7 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open7 = true;
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
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            $scope.mandatorycolending = false;
            $scope.rdbpaycard = 'Not Applicable';
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCad/GetBuyerList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.rmbuyer_list = resp.data.rmbuyer_list;
                $scope.creditbuyer_list = resp.data.creditbuyer_list;
            });
            
            var url = 'api/AgrTrnContract/tempcontractdelete';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();

            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCAD/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtentity_gid = resp.data.entity_gid;
                $scope.txtentity_name = resp.data.entity_name;
                $scope.txtcreditapproved_date = resp.data.approved_date;
                $scope.txtvertical_code = resp.data.vertical_code;
            });


            var url = 'api/AgrTrnProductApproval/GetMemberMangerApprovalDtls';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.product_membername = resp.data.member_name;
                $scope.productmember_approvaldate = resp.data.memberapproval_date;
                $scope.productmember_approvalflag = resp.data.memberapproval_flag;
                $scope.product_managername = resp.data.manager_name;
                $scope.productmanager_approvaldate = resp.data.manager_approvaldate;
                $scope.productmanager_approvalflag = resp.data.manager_approvalflag;
                $scope.productmanager_approvalremarks = resp.data.manager_approvalremarks;
                unlockUI();
            });
            // Get CC Member List
            var url = 'api/AgrTrnCC/GetScheduleMeeting';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmember_list = resp.data.ccmember_list;
                $scope.otheruser_list = resp.data.otheruser_list;
            });
            //Facility Type Drop Down
            var url = 'api/MstCAD/GetProductList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.loanfacility_list = resp.data.producttype_list;
            });

            //Sanction Type Drop Down
            var url = 'api/MstCAD/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanctiontype_list = resp.data.sanctiontype_list;
            });

            // Get Primary Mobile No, Mail ID
            var url = 'api/MstCADApplication/GetMobileMailDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimary_mobileno = resp.data.primary_mobileno;
                $scope.txtprimary_email = resp.data.primary_email;
            });
            
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.txtcreditapproved_date = resp.data.approved_date;
                $scope.lblbusinessapproved_date = resp.data.lblbusinessapproved_date;
                $scope.txtbusinessapproved_date = resp.data.businessapproved_date;
                $scope.txtccapproved_date = resp.data.ccapproved_date;
                $scope.txtccgroup_name = resp.data.ccgroup_name;
                $scope.txtregion = resp.data.region;
                $scope.txtcontract_id = resp.data.contract_id;
                $scope.txtvalidityfrom_date = resp.data.validityfrom_date;
                $scope.txtvalidityto_date = resp.data.validityto_date;
                $scope.onboard_gid = resp.data.buyeronboard_gid;
                $scope.txtbuyeragreement_id = resp.data.buyeragreement_id;

            });

            var url = 'api/AgrTrnAppCreditUnderWriting/Getproductprogramgid';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsproduct_gid = resp.data.product_gid;
                $scope.lsprogram_gid = resp.data.program_gid;

            });

            var url = 'api/AgrTrnAppCreditUnderWriting/Getrenewalamendmentflag';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsrenewal_flag = resp.data.renewal_flag;
                $scope.lsamendment_flag = resp.data.amendment_flag;

            });

            // Get Overall Limit
            var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtSanctionAmount = resp.data.overalllimit_amount;
            });
            // Get Credit Approval Hirerichy
            var url = 'api/AgrTrnCreditApproval/Getcreditheadsview';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcredit_head = resp.data.credithead_name;
                $scope.txtnational_manager = resp.data.nationalcredit_name;
                $scope.txtregional_manager = resp.data.regionalcredit_name;
                $scope.txtcredit_manager = resp.data.creditmanager_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.remarks = resp.data.remarks;
                unlockUI();
            });
            //get Product Approval hierarchy
            var url = 'api/AgrTrnProductApproval/GetAppProductAprovalinfo';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.productdesk_name = resp.data.productdesk_name;
                $scope.product_managername = resp.data.product_managername;
                $scope.product_membername = resp.data.product_membername;

            });
            // Get MOM and CAM Document
            var url = 'api/AgrTrnCAD/GetMOMCAMDocument';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.momuploaddocument_list = resp.data.cadmomdocument_list;
                $scope.camuploaddocument_list = resp.data.cadcamdocument_list;
            });
            var param = {
                application_gid: application_gid,
                employee_gid: employee_gid,
            }
            // Get RM Approval Hirerichy
            var url = 'api/AgrTrnApplicationApproval/Getapplicationhierarchylist';

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.clusterhead;
                $scope.zonalhead = resp.data.zonalhead;
                $scope.regionhead = resp.data.regionhead;
                $scope.businesshead = resp.data.businesshead;
            });

            $scope.customer_pnl = false;

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;
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
            refreshproductdetails();

            var url = 'api/MstLSA/lsabranch';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.branch_list = resp.data.branch_list;
            });

          
            var params = {
                application2sanction_gid: lsapplication2sanction_gid
            }
            ////Template
            var url = 'api/AgrTrnContract/GetCADTemplateDetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionletter_status = resp.data.sanctionletter_status;
                $scope.template_name = resp.data.template_name;
                $scope.template_gid = resp.data.template_gid;
                $scope.lspath = resp.data.makerfile_path;
                $scope.lsname = resp.data.makerfile_name;
                $scope.showmstcontent = resp.data.mstcontent_flag;
                if (resp.data.mstcontent_flag == "Y") {
                    $scope.MdlTemplatelist = resp.data.MdlTemplatedtl;
                }
                $scope.sanctionletter_flag = resp.data.sanctionletter_flag;
                if ($scope.sanctionletter_flag == 'Y')
                    $scope.content = resp.data.template_content;
                else
                    $scope.content = resp.data.defaulttemplate_content;
                document.getElementById('sanctiontemplate').innerHTML += $scope.content;
                $scope.checkerpushback_remarks = resp.data.checkerpushback_remarks;
                $scope.sanction_template = true;
                $scope.sanction_template_bind = true;

                unlockUI();
            });

            var url = 'api/AgrTrnContract/CADContractLetterSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });
        }

        function refreshloandetails() {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
           
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;
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
            refreshproductdetails();
        }

        function refreshproductdetails() {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstCAD/GetSanctionLimitInfoDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.productdetails = resp.data.limitandproducts;
            });
        }

        $scope.sanctiontype_change = function (cbosanction_type) {
            var params = {
                sanctiontype_name: $scope.cbosanction_type.sanctiontype_name,
                application_gid: application_gid,
            }
            var url = 'api/AgrTrnCad/GetSanctionToList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();
                $scope.sanctionto_list = resp.data.sanctionto_list;
            });
        }
        $scope.sanctionto_change = function (cbosanctionto) {
            var params = {
                sanctionto_gid: $scope.cbosanctionto.sanctionto_gid,
            }
            lockUI();
            var url = 'api/AgrTrnCad/GetContactPersonDetail';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimary_address = resp.data.primary_address;
                $scope.txtcontactperson_name = resp.data.contactperson_name;

                $scope.cadmobileno_list = resp.data.cadmobileno_list;
                $scope.cademail_list = resp.data.cademail_list;
                $scope.cadaddress_list = resp.data.cadaddress_list;
            });
        }

        $scope.ngclickevent = function () {
            $scope.mandatorycolending = false;
        }


        $scope.rdbdeclaration_yes = function () {
            $scope.esdeclarationyes = true;
            $scope.esdeclarationno = false;
        }
        $scope.rdbdeclaration_no = function () {
            $scope.esdeclarationyes = false;
            $scope.esdeclarationno = true;
        }

        $scope.cc_remarksview = function (ccmeeting2members_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewccremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    application_gid: application_gid,
                    ccmeeting2members_gid: ccmeeting2members_gid
                }
                var url = 'api/AgrTrnCC/ViewCCRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblremarks = resp.data.remarks;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }
        //Contract Add Document Upload

        $scope.ContractDocumentUpload = function (val, val1, name) {
          
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
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/AgrTrnContract/ContractDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file1").val('');
                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    //var param = {
                    //    application2sanction_gid: $scope.application2sanction_gid
                    //};
                    //var url = 'api/AgrTrnContract/ContractDocList';
                    //SocketService.getparams(url, param).then(function (resp) {
                    //    $scope.upload_list = resp.data.Contractupload_list;
                    //});
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
            $scope.upload_list = '';
        }

       //Procceed To Checker

        $scope.proceedtochecker = function () {
            lockUI();
            //var previewtemplate_content = getPreviewcontent($scope.content);
            var param = {
                //sanction_gid: sanction_gid,
                //template_content: previewtemplate_content,
                //contract_id: $scope.txtcontract_id,
                //contract_date: $scope.txtSanctionDate,
                application_gid: application_gid,
                application2sanction_gid: $scope.lsapplication2sanction_gid
            };
            var url = 'api/AgrTrnContract/PostProceedToChecker';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                   
                    $location.url('app/AgrMstContractSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        //Contrate Generate start

        // Template Updation
        
        // Sanction Letter Save Event
        $scope.sanctionletterSave = function () {
            lockUI();
            var previewtemplate_content = getPreviewcontent($scope.content);

            var param = {
                sanction_gid: sanction_gid,
                template_content: previewtemplate_content,
                defaulttemplate_content: $scope.content
            };
            var url = 'api/AgrTrnContract/CADContractLetterSave';
            SocketService.post(url, param).then(function (resp) {
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
                    unlockUI();
                }
            });
        }

        function extractContent(s) {
            var span = document.createElement('span');
            span.innerHTML = s;
            var txtx = span.innerHTML;
            var children = span.querySelectorAll('*');
            var task = {
                list: []
            }
            for (var i = 0; i < children.length; i++) {
                var getinputdata = "";
                if (children[i].classList.contains("froalaeditor-textbox")) {
                    getinputdata = children[i];
                }
                else if (children[i].classList.contains("froalaeditor-textarea")) {
                    getinputdata = children[i];
                }
                else {
                    getinputdata = "";
                }
                if (getinputdata != "") {
                    var inputgroup_gid = getinputdata.id;
                    var inputgroup_name = getinputdata.name;
                    var inputmax_length = (getinputdata.maxLength == -1) ? "" : getinputdata.maxLength
                    var input_placeholder = getinputdata.placeholder;
                    var input_type = getinputdata.type;

                    task.list.push({
                        inputgroup_gid: inputgroup_gid,
                        inputgroup_name: inputgroup_name,
                        inputmax_length: inputmax_length,
                        input_placeholder: input_placeholder,
                        input_type: input_type
                    });
                }
            }
            return task;
        };


        // Sanction Letter Submit Event
        $scope.sanctionletterSubmit = function () {
            lockUI();
            var previewtemplate_content = getPreviewcontent($scope.content);
            var param = {
                sanction_gid: sanction_gid,
                template_content: previewtemplate_content,
                defaulttemplate_content: $scope.content
            };
            var url = 'api/AgrTrnContract/DaCADContractLetterSubmit';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $location.hash('sanctionlettertopview');
                    $anchorScroll();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        // Sanction Letter View
        $scope.sanctionletterview = function () {
            $location.url('app/AgrMstAppContractLetterWordView?sanction_gid=' + sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
        }

        // Sanction Letter Moved to Checker
    
        // Sanction Letter View
        $scope.sanctiontocheckerview = function (sanctionapprovallog_gid, application2sanction_gid) {
            localStorage.setItem('RefreshTemplate', 'N');
            $location.url('app/AgrMstAppContractLetterWordView?sanctionapprovallog_gid=' + sanctionapprovallog_gid + '&sanction_gid=' + sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
        }
        $scope.titleOptions = {
            placeholderText: 'Add a Title',
            charCounterCount: false,
            toolbarInline: true,
            events: {
                'contentChanged': function (e, editor) {
                    console.log('content changed', $scope.titleOptions.froalaEditor.html.get());
                },
                'initialized': function (editor) {
                    console.log('initialized', this);
                }
            }
        };
        $scope.initialize = function (initControls) {
            $scope.initControls = initControls;
            $scope.deleteAll = function () {
                initControls.getEditor().html.set('34434');
            };
        };
        $scope.imgModel = { src: 'image.jpg' };
        $scope.buttonModel = { innerHTML: 'Click Me' };
        $scope.inputModel = { placeholder: 'I am an input!' };
        $scope.inputOptions = {
            angularIgnoreAttrs: ['class', 'ng-model', 'id', 'froala']
        }
        $scope.initializeLink = function (linkInitControls) {
            $scope.linkInitControls = linkInitControls;
        };
        $scope.linkModel = { href: 'https://www.froala.com/wysiwyg-editor' }

        new FroalaEditor('div#froala-editor', {
            heightMin: 900,
            heightMax: 1500,
            charCounterCount: false,
            quickInsertEnabled: false,
            imageMove: false,
            imageDefaultWidth: 0,
            imageDefaultAlign: 'left',
            imageUploadURL: imgurl,
            toolbarButtons: ['Fullscreen', 'bold', 'italic', 'underline', 'strikeThrough',
                'subscript', 'superscript', 'fontFamily', 'fontSize', 'inlineStyle', 'paragraphStyle',
                'paragraphFormat', 'align', 'formatOL', 'formatUL', 'outdent', 'indent', 'insertLink', 'insertImage',
                'insertVideo', 'insertTable', 'quote', 'insertHR', 'undo', 'redo', 'clearFormatting',
                'selectAll', 'html'],
            key: froalaConfigKey,
            events: {
                initialized: function () {
                    this.edit.off();
                }
            }
        })
        new FroalaEditor('div#froala-editorpreview', {
            heightMin: 900,
            heightMax: 1500,
            charCounterCount: false,
            quickInsertEnabled: false,
            imageMove: false,
            imageDefaultWidth: 0,
            imageDefaultAlign: 'left',
            imageUploadURL: imgurl,
            toolbarButtons: ['Fullscreen', 'bold', 'italic', 'underline', 'strikeThrough',
                'subscript', 'superscript', 'fontFamily', 'fontSize', 'inlineStyle', 'paragraphStyle',
                'paragraphFormat', 'align', 'formatOL', 'formatUL', 'outdent', 'indent', 'insertLink', 'insertImage',
                'insertVideo', 'insertTable', 'quote', 'insertHR', 'undo', 'redo', 'clearFormatting',
                'selectAll', 'html'],
            key: froalaConfigKey,
            events: {
                initialized: function () {
                    this.edit.off();
                }
            }
        })
        $scope.editfielddetail = function (content) {
            var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
            editor1.html.set(content);
            // var getinputarray =  extractContent(content);  
            // $scope.inputlist = getinputarray.list;
            $scope.showedittemplate = true;
            $scope.showpreviewtemplate = false;
            $scope.showdefaulttemplate = false;
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
            lockUI();
            var url = "api/AgrTemplate/GetTrnInputList"
            var param = {
                template_gid: $scope.template_gid,
                templatetype_gid: sanction_gid,
            };
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.inputlist = resp.data.MdlInputDtls;
                    angular.forEach($scope.inputlist, function (value, key) {
                        value.DBInput = false;
                        if (value.input_type == 'checkbox' && value.input_givendata == "true") {
                            value.input_givendata = true;
                        }
                    });

                    var url = "api/AgrTemplate/GetTrnDBInputList"
                    var param = {
                        template_gid: $scope.template_gid,
                        templatetype_gid: sanction_gid,
                        application_gid: application_gid
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.inputDBlist = resp.data.MdlDBInputDtls;
                            $scope.DefaultinputDBlist = resp.data.MdlDBInputDtls;
                            angular.forEach($scope.inputDBlist, function (value, key) {
                                value.DBInput = true;
                            });
                            var getDBdropdowndtl = $scope.inputDBlist.filter(function (el) { return el.input_type == "select-one" });
                            $scope.inputlist = $scope.inputlist.concat(getDBdropdowndtl);

                            var span = document.createElement('span');
                            span.innerHTML = content;
                            var children = span.querySelectorAll('*');
                            for (var i = 0; i < children.length; i++) {
                                if (children[i].classList.contains("froalaeditor-DBFieldtextbox")) {
                                    var getgroupdtl = $scope.inputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });
                                    if (getgroupdtl != null) {
                                        span.innerHTML = span.innerHTML.replace(children[i].outerHTML, getgroupdtl[0].input_fieldvalue);
                                    }
                                }
                            }
                            var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
                            editor1.html.set(span.innerHTML);
                        }
                        else
                            unlockUI();
                    });
                }
                else {
                    var url = "api/AgrTemplate/GetTrnDBInputList"
                    var param = {
                        template_gid: $scope.template_gid,
                        templatetype_gid: sanction_gid,
                        application_gid: application_gid
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.inputDBlist = resp.data.MdlDBInputDtls;
                            $scope.DefaultinputDBlist = resp.data.MdlDBInputDtls;
                            angular.forEach($scope.inputDBlist, function (value, key) {
                                value.DBInput = true;
                            });
                            var getDBdropdowndtl = $scope.inputDBlist.filter(function (el) { return el.input_type == "select-one" });
                            $scope.inputlist = getDBdropdowndtl;

                            var span = document.createElement('span');
                            span.innerHTML = content;
                            var children = span.querySelectorAll('*');
                            for (var i = 0; i < children.length; i++) {
                                if (children[i].classList.contains("froalaeditor-DBFieldtextbox")) {
                                    var getgroupdtl = $scope.inputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });
                                    if (getgroupdtl != null) {
                                        span.innerHTML = span.innerHTML.replace(children[i].outerHTML, getgroupdtl[0].input_fieldvalue);
                                    }
                                }
                            }
                            var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
                            editor1.html.set(span.innerHTML);
                        }
                        else
                            unlockUI();
                    });
                }
                unlockUI();
            });

            $scope.updateinputTemplate = function () {
                // console.log($scope.inputlist)
                lockUI();
                var url = "api/AgrTemplate/PostTrnInputList"
                var param = {
                    template_gid: $scope.template_gid,
                    templatetype_gid: sanction_gid,
                    MdlInputDtls: $scope.inputlist,
                };
                SocketService.post(url, param).then(function (resp) {
                    if (resp.data.status == true) {


                        $scope.inputdetails = resp.data.MdlInputDtls;
                        var span = document.createElement('span');
                        span.innerHTML = content;
                        var children = span.querySelectorAll('*');
                        for (var i = 0; i < children.length; i++) {
                            if (children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                                if (getgroupdtl != null) {
                                    children[i].innerHTML = getgroupdtl[0].input_givendata;
                                    children[i].defaultValue = getgroupdtl[0].input_givendata;
                                }
                            }
                            else if (children[i].classList.contains("froalaeditor-dropdown")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                                if (getgroupdtl != null && getgroupdtl.length != 0) {
                                    var options = children[i].options;
                                    for (var j = 0; j < options.length; j++) {
                                        if (options[j].value === getgroupdtl[0].input_givenvalue) {
                                            var replaceValue = " value=\"" + options[j].value + "\" selected=true";
                                            var existingvalue = " value=\"" + options[j].value + "\"";
                                            children[i].innerHTML = children[i].innerHTML.replace(existingvalue, replaceValue);
                                        }
                                    }
                                }
                            }
                            else if (children[i].classList.contains("froalaeditor-DBFielddropdown")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid == children[i].id });
                                if (getgroupdtl != null && getgroupdtl[0].input_givenvalue != undefined) {
                                    var options = children[i].options;
                                    var getvalue = "";
                                    for (var j = 0; j < options.length; j++) {
                                        getvalue = options[j].value
                                    }
                                    var option = "";
                                    option += '<option value="">' + getgroupdtl[0].input_givenvalue + "</option>";
                                    var data = ' &nbsp;<select class="froalaeditor-DBFielddropdown"   id="' + children[i].id + '" name="' + children[i].placeholder + '"' +
                                        ' placeholder="' + children[i].placeholder + '" disabled="true" style="font-size:13px;font-family:Calibri;"' +
                                        ' type="dropdown">' + option + "</select>&nbsp;";
                                    children[i].innerHTML = children[i].innerHTML.replace(children[i].innerHTML, data);
                                }
                            }
                            else if (children[i].classList.contains("froalaeditor-DBFieldtextbox")) {
                                var getgroupdtl = $scope.inputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });
                                if (getgroupdtl != null) {
                                    span.innerHTML = span.innerHTML.replace(children[i].outerHTML, getgroupdtl[0].input_fieldvalue);
                                }
                            }
                            else if (children[i].classList.contains("froalaeditor-checkbox")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                                var test = children[i];

                                if (getgroupdtl[0].input_givenvalue == "true") {
                                    var replaceValue = " value=\"\" checked=true";
                                    var existingvalue = " value=\"\"";
                                }
                                else {
                                    var replaceValue = " value=\"\"";
                                    var existingvalue = " value=\"\" checked=\"true\"";
                                }
                                children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                            }
                            else if (children[i].classList.contains("froalaeditor-radiobutton")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                                if (getgroupdtl != null && getgroupdtl.length != 0) {
                                    if (children[i].value.trim() === getgroupdtl[0].input_givenvalue.trim()) {
                                        var replaceValue = "value=\"" + getgroupdtl[0].input_givenvalue + "\" checked=\"true\"";
                                        var existingvalue = "value=\"" + getgroupdtl[0].input_givenvalue + "\"";
                                        children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                                    }
                                    else {
                                        var replaceValue = "";
                                        var existingvalue = "checked=\"true\"";
                                        children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                                    }
                                }
                            }
                        }
                        $scope.content = span.innerHTML;
                        var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
                        editor1.html.set($scope.content);
                        var previewtemplate_content = getPreviewcontent($scope.content);
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var param = {
                            sanction_gid: sanction_gid,
                            template_content: previewtemplate_content,
                            defaulttemplate_content: $scope.content
                        };

                        var url = 'api/AgrTrnContract/CADContractLetterSave';
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                        });
                        // console.log($scope.content);
                        // console.log('innerHTML',span.innerHTML);
                        // $('div#froala-editor').FroalaEditor('html.set', span.innerHTML);
                        //$scope.refreshfielddetail();

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                });
            }
        }
        $scope.closetemplate = function () {
            $scope.showedittemplate = false;
            $scope.showpreviewtemplate = false;
            $scope.showdefaulttemplate = true;
            if ($scope.DefaultinputDBlist && $scope.DefaultinputDBlist.length != 0) {
                var span = document.createElement('span');
                span.innerHTML = content;
                var children = span.querySelectorAll('*');
                for (var i = 0; i < children.length; i++) {
                    if (children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea")) {
                        var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                        if (getgroupdtl != null) {
                            children[i].innerHTML = getgroupdtl[0].input_givendata;
                            children[i].defaultValue = getgroupdtl[0].input_givendata;
                        }
                    }
                    else if (children[i].classList.contains("froalaeditor-dropdown")) {
                        var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                        if (getgroupdtl != null && getgroupdtl.length != 0) {
                            var options = children[i].options;
                            for (var j = 0; j < options.length; j++) {
                                if (options[j].value === getgroupdtl[0].input_givenvalue) {
                                    var replaceValue = " value=\"" + options[j].value + "\" selected=true";
                                    var existingvalue = " value=\"" + options[j].value + "\"";
                                    children[i].innerHTML = children[i].innerHTML.replace(existingvalue, replaceValue);
                                }
                            }
                        }
                    }
                    else if (children[i].classList.contains("froalaeditor-DBFielddropdown")) {
                        var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid == children[i].id });
                        if (getgroupdtl != null && getgroupdtl[0].input_givenvalue != undefined) {
                            var options = children[i].options;
                            var getvalue = "";
                            for (var j = 0; j < options.length; j++) {
                                getvalue = options[j].value
                            }
                            var option = "";
                            option += '<option value="">' + getgroupdtl[0].input_givenvalue + "</option>";
                            var data = ' &nbsp;<select class="froalaeditor-DBFielddropdown"   id="' + children[i].id + '" name="' + children[i].placeholder + '"' +
                                ' placeholder="' + children[i].placeholder + '" disabled="true" style="font-size:13px;font-family:Calibri;"' +
                                ' type="dropdown">' + option + "</select>&nbsp;";
                            children[i].innerHTML = children[i].innerHTML.replace(children[i].innerHTML, data);
                        }
                    }
                    else if (children[i].classList.contains("froalaeditor-DBFieldtextbox")) {
                        var getgroupdtl = $scope.inputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });
                        if (getgroupdtl != null) {
                            span.innerHTML = span.innerHTML.replace(children[i].outerHTML, getgroupdtl[0].input_fieldvalue);
                        }
                    }
                    else if (children[i].classList.contains("froalaeditor-checkbox")) {
                        var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                        var test = children[i];

                        if (getgroupdtl[0].input_givenvalue == "true") {
                            var replaceValue = " value=\"\" checked=true";
                            var existingvalue = " value=\"\"";
                        }
                        else {
                            var replaceValue = " value=\"\"";
                            var existingvalue = " value=\"\" checked=\"true\"";
                        }
                        children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                    }
                    else if (children[i].classList.contains("froalaeditor-radiobutton")) {
                        var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                        if (getgroupdtl != null && getgroupdtl.length != 0) {
                            if (children[i].value.trim() === getgroupdtl[0].input_givenvalue.trim()) {
                                var replaceValue = "value=\"" + getgroupdtl[0].input_givenvalue + "\" checked=\"true\"";
                                var existingvalue = "value=\"" + getgroupdtl[0].input_givenvalue + "\"";
                                children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                            }
                            else {
                                var replaceValue = "";
                                var existingvalue = "checked=\"true\"";
                                children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                            }
                        }
                    }
                }
                $scope.content = span.innerHTML;
            }
        }
        $scope.previewcontent = function (templatecontent) {
            $scope.showedittemplate = false;
            $scope.showpreviewtemplate = true;
            $scope.showdefaulttemplate = false;
            var span = document.createElement('span');
            span.innerHTML = templatecontent;
            var children = span.querySelectorAll('*');
            for (var i = 0; i < children.length; i++) {
                if (children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea") || children[i].classList.contains("froalaeditor-dropdown")) {
                    span.innerHTML = span.innerHTML.replace(children[i].outerHTML, children[i].value);
                }
                if (children[i].classList.contains("froalaeditor-DBFieldtextbox")) {
                    var getgroupdtl = $scope.DefaultinputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });
                    if (getgroupdtl != null) {
                        var data = (getgroupdtl[0].input_givendata == null || getgroupdtl[0].input_givendata == "") ? getgroupdtl[0].input_givendata : getgroupdtl[0].input_fieldvalue;
                        span.innerHTML = span.innerHTML.replace(children[i].outerHTML, data);
                    }
                }
                if ($scope.inputlist && $scope.inputlist.length != 0) {
                    if (children[i].classList.contains("froalaeditor-DBFielddropdown")) {
                        var getgroupdtl = $scope.inputlist.filter(function (el) { return el.input_fieldid == children[i].id });
                        if (getgroupdtl != null) {
                            span.innerHTML = span.innerHTML.replace(children[i].outerHTML, getgroupdtl[0].input_givendata);
                        }
                    }
                }

            }
            var editor1 = new FroalaEditor('div#froala-editorpreview', {}, function () { })
            editor1.html.set(span.innerHTML);
        }
        function getPreviewcontent(templatecontent) {
            var span = document.createElement('span');
            span.innerHTML = templatecontent;
            var children = span.querySelectorAll('*');
            for (var i = 0; i < children.length; i++) {
                if (children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea") || children[i].classList.contains("froalaeditor-dropdown")) {
                    span.innerHTML = span.innerHTML.replace(children[i].outerHTML, children[i].value);
                }
            }
            return span.innerHTML;
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


        $scope.changeTemplate = function () {
            $scope.content = $scope.cbotemplate.template_content;
        }
        //$scope.TemplateConfirm = function () {
        //    var params = {
        //        template_gid: $scope.cbotemplate.template_gid,
        //        templatetype_gid: sanction_gid,
        //        lstemplatefrom: 'Sanction'
        //    }
        //    var url = 'api/AgrTrnContract/PostTrnTemplateConfirm';
        //    lockUI();
        //    SocketService.post(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status == true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            activate();
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //    });
        //}

       //Contrate Generate End


        $scope.downloadsCAM = function (val1, val2) {
           
           DownloaddocumentService.Downloaddocument(val1, val2);

       }

        $scope.downloadsMOM = function (val1, val2) {
          
           DownloaddocumentService.Downloaddocument(val1, val2);

        }
       $scope.downloads = function (val1, val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadall = function () {
           for (var i = 0; i < $scope.upload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
           }
        }

        $scope.downloadallcam = function () {
            for (var i = 0; i < $scope.camuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.camuploaddocument_list[i].document_path, $scope.camuploaddocument_list[i].document_name);
            }
         }

        $scope.downloadallmom = function () {
            for (var i = 0; i < $scope.momuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.momuploaddocument_list[i].document_path, $scope.momuploaddocument_list[i].document_name);
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
        //$scope.uploaddocumentcancel = function (contractdocumentupload_gid) {
        //    lockUI();
        //    var params = {
        //        contractdocumentupload_gid: contractdocumentupload_gid
        //    }
        //    var url = 'api/AgrTrnContract/TmpDocumentDelete';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        $scope.upload_list = resp.data.upload_list;
        //        if (resp.data.status == true) {

        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });

        //        }
        //        unlockUI();
        //    });
        //}
        $scope.uploaddocumentcancel = function (contractdocumentupload_gid) {
            lockUI();
            var params = {
                contractdocumentupload_gid: contractdocumentupload_gid
            }
            var url = 'api/AgrTrnContract/TmpDocumentDelete';
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
                    application2sanction_gid: lsapplication2sanction_gid
                };
                var url = 'api/AgrTrnContract/ContractSummaryDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    //$scope.lrfilename = resp.data.filename;
                    //$scope.lrfilepath = resp.data.filepath;
                    $scope.upload_list = resp.data.upload_list;
                });
                unlockUI();
            });
        }


        $scope.downloadallupload = function () {
            for (var i = 0; i < $scope.uploadesfilename_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.uploadesfilename_list[i].document_path, $scope.uploadesfilename_list[i].document_name);
            }
        }



        $scope.downloadallmail = function () {
            for (var i = 0; i < $scope.mailfilename_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.mailfilename_list[i].document_path, $scope.mailfilename_list[i].document_name);
            }
        }
       // // Numeric to Word - Indian Standard...//
       

       // $scope.sanctiontype_existing = function () {
       //     $scope.existing_customer = true;
       // }

       // $scope.sanctiontype_new = function () {
       //     $scope.existing_customer = false;
       // }

       // $scope.interchangeability_yes = function () {
       //     $scope.interchangeabilityno = false;
       //     $scope.interchangeabilityyes = true;
       //     $scope.mandatoryfields = false;
       // }
       // $scope.interchangeability_no = function () {
       //     $scope.interchangeabilityno = true;
       //     $scope.interchangeabilityyes = false;
       //     $scope.mandatoryfields = false;
       // }

        $scope.onselectedchangeccgroup = function () {
            var params = {
                ccgroupname_gid: $scope.cboccgroup_name.ccgroupname_gid
            }
            var url = 'api/AgrMstCCMember/Getccgroup2member';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ccmember_list = resp.data.ccmember_list;
            });
        }




        $scope.Back = function (val) {
            if (lspage == 'ContractMaker') {
                $location.url('app/AgrMstContractDtlSummary?application_gid=' + application_gid + '&lsemployee_gid=' + employee_gid + '&lspage=ContractMaker');
            }
            else {

            }
        }




     
       // //Sanction Submit Event
        $scope.sanctionSubmit = function () {
                var params = {
                    contract_id: $scope.txtcontract_id,
                    validityfrom_date: $scope.txtvalidityfrom_date,
                    validityto_date: $scope.txtvalidityto_date,
                    contract_date: $scope.txtSanctionDate,                  
                    application_gid: application_gid
                   
                }
            var url = 'api/AgrTrnContract/PostCADSanction';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        
                        Notify.alert(resp.data.message, 'success')
                        
                            
                        $location.url('app/AgrMstContractDtlSummary?application_gid=' + application_gid + '&lsemployee_gid=' + employee_gid  + '&lspage=ContractMaker');
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }
                    activate();
                });
        
           
        }

        $scope.onboardappdetailinfo = function (onboard_gid) {
            $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lspage=' + lspage + '&lsparent=AppRMView'));
        }

        
        $scope.productcomaparisonview = function (onboard_gid, program_gid, product_gid) {
            //$location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL("&onboard_gid=" + onboard_gid));
            $location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lspage=' + lspage + '&lsparent=AppRMView' + '&product_gid=' + product_gid + '&program_gid=' + program_gid));

        }
        $scope.commodity_view = function (application2product_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/CommodityViewDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService', 'cmnfunctionService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService, cmnfunctionService) {
                lockUI();
                var params = {
                    application2product_gid: application2product_gid
                }
                var url = 'api/AgrMstApplicationEdit/GetAppCommodityDtls';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commoditydtls = resp.data;
                        unlockUI();
                    }
                });
                lockUI();
                var params = {
                    application2product_gid: application2product_gid
                }
                var url = 'api/AgrMstApplicationEdit/GetAppCommodityGstList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commoditygststatuslist = resp.data.commoditygststatus;
                        unlockUI();
                    }
                });
                var url = 'api/AgrMstApplicationEdit/GetAppCommodityTradeProdctList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commodityTradeProdctlist = resp.data.commodityTradeProdct;
                        unlockUI();
                    }
                });

                var url = 'api/AgrMstApplicationEdit/GetAppCommodityCustomerpaymentList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commoditycustomerpayment = resp.data.commoditycustomerpayment;
                        unlockUI();
                    }
                });

                var url = 'api/AgrMstApplicationEdit/GetAppCommodityDocumentUploadList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commodityDocumentUpload = resp.data.commodityDocumentUpload;
                        unlockUI();
                    }
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.doc_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_8 = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].commodityreport_filepath, $scope.commodityDocumentUpload[i].commodityreport_filename);
                    }
                }
                $scope.downloadall_9 = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].riskanalysisreport_filepath, $scope.commodityDocumentUpload[i].riskanalysisreport_filename);
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


            }
        }

        $scope.tradedtl_view = function (application2trade_gid) {
            if ($scope.tradedtl_view_flag == undefined || $scope.tradedtl_view_flag == '') {
                $scope.tradedtl_view_flag = true;
            }
            else if ($scope.tradedtl_view_flag == true) {
                $scope.tradedtl_view_flag = false;
            }
            else { $scope.tradedtl_view_flag = true; }
            $scope.application2trade_gid = application2trade_gid;
            $scope.TradeEditdivshow = true;
            var params = {
                application2trade_gid: application2trade_gid
            }
            lockUI();
            var url = 'api/AgrMstApplicationAdd/GetApplicationTradeViewdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboeditproduct_type = resp.data.producttype_name;
                $scope.txteditProductsubType = resp.data.productsubtype_name;
                $scope.rdbeditsalescontract_availability = resp.data.salescontract_availability;
                $scope.cboeditScopeoftransport = resp.data.scopeof_transport;
                $scope.cboeditScopeofloading = resp.data.scopeof_loading;
                $scope.cboeditScopeofunloading = resp.data.scopeof_unloading;
                $scope.cboeditScopeofqualityandquantity = resp.data.scopeof_qualityandquantity;
                $scope.cboeditScopeofmoisturegainloss = resp.data.scopeof_moisturegainloss;
                $scope.cboScopeofInsurance = resp.data.scopeof_insurance;
                unlockUI();
            });

            var params = {
                application2trade_gid: application2trade_gid,
                application_gid: $scope.application_gid,
                tmp_status: "true",
                application2loan_gid: ''
            }

            var url = 'api/AgrMstApplicationAdd/GetTrade2WarehouseTmpDetail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tradewarehouse_list = resp.data.creditor2warehouse_list;
            });

            var url = 'api/AgrMstCreditorMaster/GetTrade2CreditorTmpDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.trade2creditor_list = resp.data.trade2creditor_list;
            });

        }

        $scope.suppliergsttrnview = function (apploan2supplierdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SupplierGSTDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var params = {
                    apploan2supplierdtl_gid: apploan2supplierdtl_gid,
                }
                var url = 'api/AgrMstApplicationEdit/GetLoan2SupplierGSTdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        $scope.SupplierGSTdtl_list = resp.data.MdlSupplierGSTdtl;
                    } else {
                        unlockUI();
                    }

                });
                                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.uploadeddoc_Collateral = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Collateraldocuments.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService', 'cmnfunctionService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService, cmnfunctionService) {
                var params =
                {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/AgrMstApplicationView/GetCollateralDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Collateraldoc_list = resp.data.CollatralDocumentList;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               

                $scope.download_Collateraldoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);

                }

                $scope.downloadallcollateral = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
                    }
                }

                $scope.downloadall_2 = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
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


            }

        }


        $scope.view = function (applicationtrade2warehouse_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    
                    applicationtrade2warehouse_gid: applicationtrade2warehouse_gid
                }
                var url = 'api/AgrMstApplicationAdd/EditTrade2WarehouseDetail';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cbowarehouseagency = resp.data.warehouse_agency;
                    $scope.cbowarehouse_name = resp.data.warehouse_name;
                    $scope.cbowarehousetype_name = resp.data.typeofwarehouse_name;
                    $scope.txtvolume_uom = resp.data.volume_uom;
                    $scope.txtcapacity_volume = resp.data.totalcapacity_volume;
                    $scope.txtareacapacity = resp.data.totalcapacity_area;
                    $scope.txtareacapacity_uom = resp.data.area_uom;
                    $scope.cbowarehouseaddress = resp.data.warehouse_address;
                    $scope.txtcapacity_commodity = resp.data.capacity_commodity;
                    $scope.txtcapacity_panina = resp.data.capacity_panina;
                    
                    unlockUI();
                });
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }    


        $scope.OtherProducts_view = function (application2loan_gid, viewproduct_type) {

            $scope.txtviewproduct_type = viewproduct_type;
            if ($scope.Products_flag == undefined || $scope.Products_flag == '') {
                $scope.Products_flag = true;
            }
            else if ($scope.Products_flag == true) {
                $scope.Products_flag = false;
            }
            else { $scope.tradedtl_view_flag = true; }
            var params1 = {
                application_gid: '',
                application2loan_gid: application2loan_gid,
                tmp_status: 'false',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2Supplierdtl';
            SocketService.getparams(url, params1).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlSupplierdtllist = resp.data.MdlSupplierdtl;
                } else {
                    unlockUI();
                }
            });
            var params = {
                application_gid: $scope.application_gid,
                application2loan_gid: application2loan_gid,
                tmp_status: 'both',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2SupplierPaymentdtl';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlSupplierPaymentlist = resp.data.MdlSupplierPaymentdtl;
                } else {
                    unlockUI();
                }

            });
            var params = {
                application2loan_gid: application2loan_gid,
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationView/GetLoanProgramValueChain';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
            });
            var params2 = {
                application_gid: $scope.application_gid,
                application2loan_gid: application2loan_gid,
                tmp_status: 'false',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2Repaymentdtl';
            SocketService.getparams(url, params2).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlrePaymentdtl = resp.data.MdlPaymentdtl;
                }
            });

            var param = {
                application_gid: $scope.application_gid,
                application2loan_gid: application2loan_gid,
            }
            var url = 'api/AgrMstApplicationEdit/GetEditLoanLimit';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;

                $scope.onboarding_status = resp.data.onboarding_status;

                if (resp.data.overalllimit_amount == "0.00" || resp.data.onboarding_status == "Direct") {

                    $scope.zerofacility = true

                    $scope.txtloanfaility_amount = '0';

                }

                else {

                    $scope.zerofacility = false

                }

                $scope.lsloanfacility_amount = resp.data.loanfacility_amount;
                if (resp.data.loanfacility_amount == '' || resp.data.loanfacility_amount == null) {
                    $scope.lsloanfacility_amount = '0';
                }
            });
            var params = {
                application2loan_gid: application2loan_gid
            }
            var url = 'api/AgrMstApplicationEdit/LoanDetailsEdit';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtfacilityreqon_date = resp.data.facilityrequested_date;
                $scope.cboProductTypelist = resp.data.producttype_gid;
                $scope.loansubproduct_name = resp.data.productsub_type;
                $scope.product_type = resp.data.product_type;
                $scope.lblproducttype = resp.data.product_type;
                $scope.lblproductsub_type = resp.data.productsub_type;
                if ($scope.lblproductsub_type == 'STF') {
                    $scope.stfmandatory = true;
                    $scope.STFdivshow = true;
                }
                else {
                    $scope.stfmandatory = false;
                    $scope.STFdivshow = false;
                }
                var params = {
                    loanproduct_gid: resp.data.producttype_gid,
                    application_gid: '',
                    application2loan_gid: ''
                }
                var url = 'api/AgrMstApplicationAdd/GetLoanSubProduct';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loansubproductlist = resp.data.application_list;
                });

                $scope.cboProductSubTypelist = resp.data.productsubtype_gid;
                $scope.cboLoanTypelist = resp.data.loantype_gid;
                $scope.loan_type = resp.data.loan_type;
                if ($scope.loan_type == 'Secured') {
                    $scope.Collateralshow = true;
                }
                else {
                    $scope.Collateralshow = false;
                }
                $scope.cboSourceType = resp.data.source_type;
                $scope.txtguidelinevalue = resp.data.guideline_value;

                $scope.txtguideline_date = resp.data.guideline_date;
                $scope.txtmarketvalue_date = resp.data.marketvalue_date;
                $scope.txtmarketValue = resp.data.market_value;


                $scope.txtforcedsource_value = resp.data.forcedsource_value;


                $scope.txtcollateralSSV_value = resp.data.collateralSSV_value;


                $scope.txtforcedvalueassessed_on = resp.data.forcedvalueassessed_on;
                $scope.txtcolateralobservation_summary = resp.data.collateralobservation_summary;

                $scope.txtloanfaility_amount = resp.data.facilityloan_amount;


                $scope.txteditrate_interest = resp.data.rate_interest;
                $scope.txteditpanel_interest = resp.data.penal_interest;
                $scope.txteditvalidity_years = resp.data.facilityvalidity_year;
                $scope.txteditvalidity_months = resp.data.facilityvalidity_month;
                $scope.txteditvalidity_days = resp.data.facilityvalidity_days;
                $scope.txtoverallfacilityvalidity_limit = resp.data.facilityoverall_limit;
                //$scope.txtedittenure_years = resp.data.tenureproduct_year;
                //$scope.txtedittenure_months = resp.data.tenureproduct_month;
                $scope.txtedittenure_days = resp.data.tenureproduct_days;
                $scope.txteditoveralllimit_validity = resp.data.tenureoverall_limit;
                $scope.cboFacilityTypelist = resp.data.facility_type;
                $scope.cboFacilitymodelist = resp.data.facility_mode;
                $scope.cboprincipalfrequency = resp.data.principalfrequency_gid;
                $scope.cboInterestFrequency = resp.data.interestfrequency_gid;
                $scope.cboProgram = resp.data.program_gid;

                $scope.valuechainlist = resp.data.valuechainlist;

                $scope.rdbmilestone_applicablity = resp.data.milestone_applicability,
                    $scope.rdbinsurance_applicability = resp.data.insurance_applicability,
                    $scope.cbomilestonepaymenttype = resp.data.milestonepayment_gid,
                    $scope.txtsapayout = resp.data.sa_payout,
                    $scope.insurance_availability = resp.data.insurance_availability,
                    $scope.txtinsurance_percent = resp.data.insurance_percent,
                    $scope.txtinsurance_cost = resp.data.insurance_cost,
                    $scope.txtnet_yield = resp.data.net_yield,
                    $scope.sa_status = resp.data.sa_status;
                if ($scope.sa_status == "Yes")
                    $scope.showsapayout = true;
                else
                    $scope.showsapayout = false;

                if ($scope.rdbmilestone_applicablity == "Yes") {
                    $scope.showmilestonepaymenttype = true;
                    $scope.disabledmilestonepaymenttype = false;
                }
                else {
                    $scope.showmilestonepaymenttype = false;
                    $scope.disabledmilestonepaymenttype = true;
                }

                $scope.rdbinterest_status = resp.data.interest_status;
                $scope.rdbmoratorium_status = resp.data.moratorium_status;
                $scope.cbomoratorium_type = resp.data.moratorium_type;
                $scope.txtmoratorium_startdate = resp.data.moratorium_startdate;
                $scope.txtmoratorium_enddate = resp.data.moratorium_enddate;
                $scope.txtenduse_purpose = resp.data.enduse_purpose;
                $scope.rdbTradeOriginated = resp.data.trade_orginatedby,
                    $scope.txtsabrokerage = resp.data.SA_Brokerage,
                    $scope.txtholdingperiod = resp.data.holding_periods,
                    $scope.txtholdingMonthlyprocurement = resp.data.holdingmonthly_procurement,
                    $scope.txtextendedholdingperiod = resp.data.extendedholding_periods,
                    $scope.txtextendedMonthlyprocurement = resp.data.extendedmonthly_procurement,
                    $scope.txtcharges_extendedperiod = resp.data.charges_extendedperiod,
                    $scope.txtcustomer_advance = resp.data.customer_advance,
                    $scope.txtreimburesementof_expenses = resp.data.reimburesementof_expenses,
                    $scope.txtreimburesementof_expensespenalty = resp.data.reimburesementof_expensespenalty,
                    $scope.bankfunding_documentname = resp.data.bankfundingdata_filename,
                    $scope.bankfunding_documentpath = resp.data.bankfundingdata_filepath,
                    $scope.txtneedfor_stocking = resp.data.needfor_stocking,
                    $scope.txtproduct_portfolio = resp.data.product_portfolio,
                    $scope.txtproduction_capacity = resp.data.production_capacity,
                    $scope.txtnatureof_operations = resp.data.natureof_operations,
                    $scope.txtaveragemonthly_inventoryholding = resp.data.averagemonthly_inventoryholding,
                    $scope.txtfinancialinstitutions_relationship = resp.data.financialinstitutions_relationship;
                $scope.txtProgramLimitValidityfrom = resp.data.programlimit_validdfrom,
                    $scope.txtProgramLimitValidityTo = resp.data.programlimit_validdto,
                    $scope.txtoverallprogramvalidity_limit = resp.data.programoverall_limit;


                var lbloveralllimit_amount = ($scope.lbloveralllimit_amount).replaceAll(',', '');
                var lsamount = (parseFloat(lbloveralllimit_amount) - parseFloat($scope.txtloanfaility_amount));
                $scope.txtremaining = parseFloat(lsamount);

                if (resp.data.product_type == 'Agri Receivable Finance (ARF)') {
                    $scope.ARF_condition = true;
                }
                else {
                    $scope.ARF_condition = false;
                }
            });


        }
      
    }
})();
