(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerReport360controller', customerReport360controller);

    customerReport360controller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', '$anchorScroll', '$sce', 'DownloaddocumentService'];

    function customerReport360controller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, $anchorScroll, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerReport360controller';
         
        activate();
        lockUI();
        function activate() {
            $scope.ShowAllocation360
          
           
            $scope.urn = localStorage.getItem('urn');
            $scope.customer_gid = localStorage.getItem('allocation_customer_gid');
            //Customer GID Base
            var param = {
                customer_gid: $scope.customer_gid
            };
           
            var url = 'api/CustomerReport/Getcustomerupdatedetails';         
            
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.customerCode = resp.data.customerCodeedit;
                $scope.customerName = resp.data.customerNameedit;
                $scope.contactPerson = resp.data.contactPersonedit;
                $scope.mobileNo = resp.data.mobileNo_edit;
                $scope.contactno = resp.data.contactno_edit;
                $scope.email = resp.data.emailedit;
                $scope.txtaddress1 = resp.data.addressline1edit;
                $scope.txtaddress2 = resp.data.addressline2edit;
                $scope.region = resp.data.regionedit;
                $scope.country = resp.data.countryedit;
                $scope.vertical = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.statename = resp.data.state;
                $scope.state_gid = resp.data.state_gid;
                $scope.riskmanager = resp.data.risk_managernameedit;
                $scope.district_name = resp.data.district_nameedit;
                $scope.postalcode = resp.data.postalcode_edit;
                $scope.tomail = resp.data.tomailedit;
                $scope.ccmail = resp.data.ccmailedit;
                $scope.zonalHead = resp.data.zonal_name;
                $scope.businessHead = resp.data.businesshead_name;
                $scope.clustermanager = resp.data.cluster_manager_name;
                $scope.creditmanager = resp.data.creditmanager_name;
                $scope.relationshipMgmt = resp.data.relationshipmgmt_name;
                $scope.customerURN = resp.data.customer_urnedit;
                $scope.pan_number = resp.data.pan_number;
                $scope.gst_number = resp.data.gst_number;
                $scope.txtmajor_corporate = resp.data.major_corporateedit;
                $scope.cboconstitution = resp.data.constitution_gidedit;
                $scope.cboconstitutionname = resp.data.constitution_nameedit;
            });
            var url = 'api/CustomerReport/Getsanctionloandetails';

            SocketService.getparams(url, param).then(function (resp) {

                $scope.sanction_list = resp.data.sanctionloanListurn;
                
                angular.forEach($scope.sanction_list, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid
                    };
                    var url = 'api/CustomerReport/GetloanFacilityDetails';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.loanfacilitytype= resp.data.loanfacilitytype_list;
                         value.expand = false;

                    });
                });
            });
            var url = 'api/customer/Getcustomerupdatedetails';           
            
            SocketService.getparams(url, param).then(function (resp) {
                $scope.zonal_riskmanagerName = resp.data.zonal_riskmanagerName;
                $scope.riskMonitoring_Name = resp.data.riskMonitoring_Name;
            });
            var url = 'api/CustomerReport/Getcustomerdetails';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.lblpan_number = resp.data.pan_no;
                $scope.customer2userdtl_list = resp.data.customer2userdtl_list;
                $scope.address_list = resp.data.address_list;
                $scope.idproof_list = resp.data.idproof_list;
                $scope.mobileno_list = resp.data.mobileno_list;
                $scope.member_list = resp.data.member_list;
                $scope.lblcustomer_type = resp.data.customer_type;
                $scope.lblgst_no = resp.data.gst_no;
                $scope.lblyear_business = resp.data.year_business;
                $scope.lblcompany_type = resp.data.company_type;
                $scope.lblcontactperson_designation = resp.data.contactperson_designation;
                $scope.lblcin_no = resp.data.cin_no;
                $scope.lblcin_date = resp.data.cin_date;
                $scope.lbllandmark = resp.data.landmark;
                $scope.lblmonth_business = resp.data.month_business;
                $scope.lblcredit_rating = resp.data.credit_rating;
                $scope.lblescrow = resp.data.escrow;
                $scope.lblage = resp.data.age;
                $scope.lblphoto_path = resp.data.photo_path;
                $scope.lblphoto_name = resp.data.photo_name;
                $scope.lblaadhar_no = resp.data.aadhar_no;
                $scope.lblcontact_person = resp.data.contact_person;
                $scope.lbltelephone_no = resp.data.telephone_no;
                $scope.lblofficailemail_address = resp.data.officailemail_address;
                $scope.lblpersonalemail_address = resp.data.personalemail_address;
                $scope.lblgender = resp.data.gender;
                $scope.lbldob = resp.data.dob;
                $scope.lblname = resp.data.name;
                $scope.lbluser_type = resp.data.user_type;

            });
            var url = "api/lglTrnDn2CustomerDetails/GetDNGenerated_History"
            lockUI();
            var param = {
                urn: $scope.urn
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.dn_list = resp.data.dn_list;
                console.log(resp.data.dn_list)
            });
            var param = {
                urn: $scope.urn
            };          
            var url = 'api/MstRepayment/GetRepayment';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.repaymentaccount_list = resp.data.repaymentaccount_list;
                angular.forEach($scope.repaymentaccount_list, function (value, key) {
                   
                    var params = {
                        account_no: value.account_no
                    };
                    var url = 'api/MstRepayment/GetRepayment_list';
                    SocketService.getparams(url, params).then(function (resp) {
                       
                        value.repayment_list = resp.data.repayment_list;
                          value.expand = false;

                    });
                });
            });
            var url = 'api/MstTelecall/GetTelecall';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.telecall_list = resp.data.telecall_list;;
            });
           
          
            var url = 'api/lglTrnDn2CustomerDetails/GetGuarantordetails';
            var param = {
                urn: $scope.urn
            };           
            SocketService.getparams(url, param).then(function (resp) {
                $scope.additional_list = resp.data.customer2userdtl_list;
            });
            

            $scope.MyZonalAllocationHistory = localStorage.getItem('MyZonalAllocationHistory');
            var customer_gid = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }
            $scope.allocation_customer_gid = customer_gid;
            
            var url = "api/allocationManagement/getAllocationHistory";
            SocketService.getparams(url, customer_gid).then(function (resp) {
                $scope.allocationHistoryList = resp.data.overallhistoryallocationdtl;
                $scope.customername = resp.data.overallhistoryallocationdtl[0].customername;
                $scope.customer_urn = resp.data.overallhistoryallocationdtl[0].customer_urn;
            });            
            $scope.viewindividualguarntor = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/viewindividualguarntor.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                }
            }

            $scope.viewguarntor = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/viewcompanyguarntor.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                }
            }

            $scope.viewtelephonicdetails = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/viewtelephonicdetails.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                }
            }

            $scope.viewrepayment = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/viewrepaymentdetails.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                }
            }
           
            var url = "api/misDataimport/getDN1Status"
           
            var param = {
                urn: $scope.urn
            };
            SocketService.getparams(url, param).then(function (resp) {
                
                $scope.tempdn1format_gid = resp.data.tempdn1format_gid;
                $scope.DN1status = resp.data.DN1status;
                $scope.DN1template_content = resp.data.DN1template_content;
                $scope.dn1status_created_by = resp.data.dn1status_created_by;
                $scope.dn1status_created_date = resp.data.dn1status_created_date;
                $scope.dn1couriersent_date = resp.data.dn1couriersent_date;
                $scope.dn1status_created_by = resp.data.dn1status_created_by;
                $scope.dn1annexuredocument_name = resp.data.dn1annexuredocument_name;
                $scope.dn1annexuredocument_path = resp.data.dn1annexuredocument_path;
                $scope.DN2status = resp.data.DN2status;
                $scope.DN2template_content = resp.data.DN2template_content;
                $scope.dn2status_created_by = resp.data.dn2status_created_by;
                $scope.dn2status_created_date = resp.data.dn2status_created_date;
                $scope.dn2couriersent_date = resp.data.dn2couriersent_date;
                $scope.dn2status_created_by = resp.data.dn2status_created_by;
                $scope.dn2annexuredocument_name = resp.data.dn2annexuredocument_name;
                $scope.dn2annexuredocument_path = resp.data.dn2annexuredocument_path;
                $scope.DN3status = resp.data.DN3status;
                $scope.DN3template_content = resp.data.DN3template_content;
                $scope.dn3status_created_by = resp.data.dn3status_created_by;
                $scope.dn3status_created_date = resp.data.dn3status_created_date;
                $scope.dn3couriersent_date = resp.data.dn3couriersent_date;
                $scope.dn3status_created_by = resp.data.dn3status_created_by;
                $scope.dn3annexuredocument_name = resp.data.dn3annexuredocument_name;
                $scope.dn3annexuredocument_path = resp.data.dn3annexuredocument_path;
            });
            var url = "api/misDataimport/getcourierinfo"
          
            var param = {
                urn: $scope.urn
            };

            SocketService.getparams(url, param).then(function (resp) {
             
                $scope.dncouriered_by = resp.data.couriered_by;
                $scope.dncourier_center = resp.data.courier_center;
                $scope.dncourier_date = resp.data.courier_date;
                $scope.dncourier_refno = resp.data.courier_refno;
                $scope.dnremarks = resp.data.courier_remarks;
                $scope.dncourier_status = resp.data.dn1courier_status;
                $scope.dn2couriered_by = resp.data.dn2couriered_by;
                $scope.dn2courier_center = resp.data.dn2courier_center;
                $scope.dn2courier_date = resp.data.dn2courier_date;
                $scope.dn2courier_refno = resp.data.dn2courier_refno;
                $scope.dn2remarks = resp.data.dn2remarks;
                $scope.dn2courier_status = resp.data.dn2courier_status;
                $scope.dn3couriered_by = resp.data.dn3couriered_by;
                $scope.dn3courier_center = resp.data.dn3courier_center;
                $scope.dn3courier_date = resp.data.dn3courier_date;
                $scope.dn3courier_refno = resp.data.dn3courier_refno;
                $scope.dn3remarks = resp.data.dn3remarks;
                $scope.dn3courier_status = resp.data.dn3courier_status;
                if (resp.data.courier_status == 'DN1 Sent') {
                    $scope.dn1couriered_by = resp.data.couriered_by;
                    $scope.dn1courier_center = resp.data.courier_center;
                    $scope.dn1courier_date = resp.data.courier_date;
                    $scope.dn1courier_refno = resp.data.courier_refno;
                    $scope.dn1remarks = resp.data.courier_remarks;
                    $scope.dn1courier_status = resp.data.dn1courier_status;
                }
                else if (resp.data.courier_status == 'DN2 Sent') {
                    $scope.dn1couriered_by = resp.data.dn2couriered_by;
                    $scope.dn1courier_center = resp.data.dn2courier_center;
                    $scope.dn1courier_date = resp.data.dn2courier_date;
                    $scope.dn1courier_refno = resp.data.dn2courier_refno;
                    $scope.dn1remarks = resp.data.dn2remarks;
                    $scope.dn1courier_status = resp.data.dn2courier_status;
                }
                else if (resp.data.courier_status == 'DN3 Sent') {
                    $scope.dn1couriered_by = resp.data.dn3couriered_by;
                    $scope.dn1courier_center = resp.data.dn3courier_center;
                    $scope.dn1courier_date = resp.data.dn3courier_date;
                    $scope.dn1courier_refno = resp.data.dn3courier_refno;
                    $scope.dn1remarks = resp.data.dn3remarks;
                    $scope.dn1courier_status = resp.data.dn3courier_status;
                }
            });
            var url = "api/misDataimport/Getrevertdetails"
           
            var param = {
                urn: $scope.urn
            };

            SocketService.getparams(url, param).then(function (resp) {
                $scope.updated_date = resp.data.updated_date;
                $scope.updated_by = resp.data.updated_by;
                $scope.dn_status = resp.data.dn_status;
                $scope.remarks = resp.data.remarks;
            });
         
            var url = "api/CustomerReport/GetCustomer2Loandtls";  

            SocketService.getparams(url, customer_gid).then(function (resp) {
            unlockUI();
                $scope.mdlMisdataimport = resp.data.mdlMisdataimport;   
                         
            });
        }
        
        $scope.cibildtlsummary = function (account_no)
        {
            $scope.cibilsummary=true;
           
            var url = 'api/CustomerReport/GetCustomerCibilSummary';
            var params = {
                account_no: account_no
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cibilsummary_dtl = resp.data.cibilsummarydtl_list;

            }); 
        }
        
        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            $location.hash('down');
            $anchorScroll();
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            unlockUI();
            $scope.ShowAllocation360 = true;
           
            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.zonal_name = resp.data.zonal_name;
                $scope.state_name = resp.data.state_name;
                $scope.district_name = resp.data.district_name;
                $scope.assigned_RM = resp.data.assigned_RM;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.ZonalRMname = resp.data.ZonalRMname;
                $scope.clientName = resp.data.customername;
            });

            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;

            });

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.customerdetails = resp.data;
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.customerCollateral = resp.data.collateraldtl;
                $scope.holdallocationlist = resp.data.holdallocation;
                $scope.customerguarantorlist = resp.data.Guarantorsdtl;

                $scope.customerPromotorlist = resp.data.Promoterdtl;
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid,
                        allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                    };

                    var url = 'api/allocationManagement/GetAllocateloanList';
                    SocketService.post(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;
                    });
                });
            });

            var url = "api/customerManagement/HistoryEscrowSummary";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowlist = resp.data.escrowSummary;
                }
            });
            lockUI()
            var url = "api/allocationManagement/getAllocationdocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                unlockUI();
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;
                    $scope.documentUpload = true;
                }
                else {

                    $scope.documentNotUpload = true;
                }
            });

            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {

                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.txtbusiness_sector = resp.data.business_sector,
                $scope.txtregister_address = resp.data.registeredoffice_address,
                $scope.cboriskcode = resp.data.risk_code,
                $scope.txtactual_address = resp.data.present_address,
                $scope.txtcontact_dtl1 = resp.data.contact_details1,
                $scope.txtcontact_dtl2 = resp.data.contact_details2,
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
                $scope.txtlattitude = resp.data.visit_latitude;
                $scope.txtlongitude = resp.data.visit_longitude;
                //$scope.firstdisb_date = resp.data.relationship_startedfrom
                $scope.txtbusiness_client = resp.data.clientbusiness_vintage,
                $scope.txtprimary_chain = resp.data.primarysecondary_valuechain,
                $scope.cbogenetic_code = resp.data.geneticcode_complied,
                $scope.cboRMD_gid = resp.data.RMD_visitedGid,
                $scope.RMD_visitedname = resp.data.RMD_visitedname;
                $scope.txtPPA_name = resp.data.PPA_name;
                $scope.cbovisit_done = resp.data.visit_done,
                $scope.txtpurposeof_loan = resp.data.purpose_ofloan,
                $scope.txtrequestedloan_byclient = resp.data.requestedamount_byclient,
                $scope.txtsantionloan_bycredit = resp.data.sanctionedamount_byclient;
                $scope.txtdisbursement_amount = resp.data.disbursement_amount,
                $scope.txttotalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.cborepayment_track = resp.data.repayment_track,
                $scope.cbobasic_records = resp.data.basicrecords_maintain,
                $scope.txtturnover_lastfy = resp.data.turnover_lastFY,
                $scope.txtpresent_fysales = resp.data.presentFY_sales,
                $scope.txtdeferral_pendency = resp.data.deferral_pendency,
                $scope.txtadditional_funding = resp.data.adequacy_additionalfunding,
                $scope.txtcbototal_groups = resp.data.total_noofGroups,
                $scope.txtCBOgroup_funded = resp.data.CBOfunded_noofGroups,
                $scope.txtRMDvisit_groupcount = resp.data.RMD_visitgroups,
                $scope.txtassetverification_comment = resp.data.assetverification_createdoutofloan,
                $scope.txtsecurity_details = resp.data.assetverification_securitydtls,
                $scope.txtassetverification_mortagged = resp.data.assetverification_mortgaged,
                $scope.txtROCcreation = resp.data.assetverification_ROCcreation,
                $scope.txtbasicrecord_remarks = resp.data.basicrecords_remarks,
                $scope.txtpurpose_funding = resp.data.purposeof_funding,
                $scope.txt_utilisationdtls = resp.data.utilisation_details,
                $scope.txtadequacyloan_samunnati = resp.data.adequacy_loanamount,
                $scope.txtadequacyloan_impactassessment = resp.data.adequacy_impactassessment,
                $scope.txtportfolio_noofmember = resp.data.portfolio_noofmembers,
                $scope.txtportfolio_activemembers = resp.data.portfolio_activemembers,
                $scope.txtportfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.txtportfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.txtportfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.txtportfolio_overdueAmount = resp.data.overdue_amount,
                $scope.txtportfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtrepayment_trackremarks = resp.data.repayment_trackremarks,
                //$scope.txtloan_clientdate = resp.data.loan_clientdate,
                $scope.txtoverdue = resp.data.overdue,
                $scope.txtborrower_commitment = resp.data.borrower_commitment,
                $scope.txtpending_documentation = resp.data.pending_documentation,
                //$scope.txtasset_verification = resp.data.asset_verification,
                $scope.txtbriefdtls_client = resp.data.briefdtls_client,
                $scope.txtenduse_loan = resp.data.enduse_loan,
                //$scope.txtadequacy_loan = resp.data.adequacy_loan,
                $scope.txtoverall_remarks = resp.data.overall_remarks,
                $scope.txtPDD_compliance = resp.data.PDD_compliance,
                $scope.txtbriefrpt_financials = resp.data.briefrpt_financials,
                $scope.txtbriefrpt_process = resp.data.briefrpt_process,
                $scope.txtbriefrpt_customer = resp.data.briefrpt_customer,
                 $scope.txtvaluechain_mapanalysis = resp.data.valuechain_mapanalysis,
                $scope.txtcompetitorbusiness_segment = resp.data.competitorbusiness_segment;
                $scope.txtbriefrpt_learnings = resp.data.briefrpt_learnings,
                $scope.txtbriefrpt_valuechain = resp.data.briefrpt_valuechain,
                $scope.editvisittype = resp.data.editvisittype;
                if (resp.data.RM_name != null) {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if (resp.data.constitution != null) {
                    $scope.constitution = resp.data.constitution
                }
                if (resp.data.credit_managername != null) {
                    $scope.credit_managername = resp.data.credit_managername;
                }
                if (resp.data.visit_date != null) {
                    var p = resp.data.visit_date.split(/\D/g)
                    $scope.visitdate = [p[2], p[1], p[0]].join("-");
                }

                if (resp.data.dealing_withsince != null) {
                    var p = resp.data.dealing_withsince.split(/\D/g)
                    $scope.txtincorporated_date = [p[2], p[1], p[0]].join("-");
                }

                if (resp.data.disbursement_date != null) {
                    var p = resp.data.disbursement_date.split(/\D/g)
                    $scope.txtdisbursement_date = [p[2], p[1], p[0]].join("-");
                }

            });

            var url = "api/visitReport/getvisitReportDocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });

            var url = "api/visitReport/getvisitReportPhoto";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
            });

            var url = "api/TierMeeting/GetViewTierObservationdtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.dateof_RMDvisit = resp.data.dateof_RMDvisit;
                $scope.report_pertainingto = resp.data.report_pertainingto;
                $scope.vertical = resp.data.vertical;
                $scope.disbursement_amount = resp.data.disbursement_amount;
                $scope.approving_authority = resp.data.approving_authority;
                $scope.loansanction_date = resp.data.loansanction_date;
                $scope.relationship_manager_name = resp.data.relationship_manager_name;
                $scope.PPA_name = resp.data.PPA_name;
                $scope.RMDvisit_officialname = resp.data.RMDvisit_officialname;
                $scope.loandisbursement_date = resp.data.loandisbursement_date;
                $scope.people_accompaniedRMD = resp.data.people_accompaniedRMD;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.outstanding_amount = resp.data.outstanding_amount;
                $scope.current_DPD = resp.data.current_DPD;
                $scope.contact_details1 = resp.data.contact_details1;
                $scope.contact_details2 = resp.data.contact_details2;
                $scope.observation_flag = resp.data.observation_flag;
                $scope.cboriskcode = resp.data.risk_code;
                $scope.criticalobservation = resp.data.criticalTierobservation;
                $scope.tiercodedtl = resp.data.tierReportdtl;

            });
            var tier1format_gid = {
                tier1format_gid: localStorage.getItem('tier1format_gid')
            }
            var url = "api/TierMeeting/GetTier1Format360Dtl";
            SocketService.getparams(url, tier1format_gid).then(function (resp) {
                $scope.txtobservations = resp.data.tier1_observations;
                if (resp.data.tier1_code == null || resp.data.tier1_code == "") {
                }
                else {
                    $scope.cboriskcode = resp.data.tier1_code;
                }
                $scope.txtrationale_justification = resp.data.tier1_justification;
                $scope.txtprocess_gap = resp.data.tier1_processgap;
                $scope.txtcode_changereason = resp.data.tier1code_changereason;
                $scope.tier1code_changeflag = resp.data.tier1code_changeflag;
                $scope.txtimprovement_recommendation = resp.data.tier1_processrecommendation;
                $scope.txtmanagement_comments = resp.data.tier1_managementcomments;
                $scope.txtcheifheadreverts_actionplan = resp.data.tier1_reverts_actionplan;
                $scope.txtATR_date = resp.data.tier1_atrdate;
                $scope.tier1format_gid = resp.data.tier1format_gid;
                $scope.tier1_approvalstatus = resp.data.tier1_approvalstatus;
                $scope.tier1approvallog = resp.data.tier1approvallog;
                $scope.uploaddocument_list = resp.data.tier1doc;
                $scope.tier1rejectlog = resp.data.tier1rejectlog;

                if ($scope.tier1code_changeflag == 'Y') {
                    $scope.disablecodechangereasonshow = true;
                }
                else {
                    $scope.disablecodechangereasonshow = false;
                }

                if (resp.data.tier1approvallog == null) {
                    $scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }
                if (resp.data.tier1rejectlog == null) {
                    $scope.rejecthistory = false;
                }
                else {
                    $scope.rejecthistory = true;
                }
            });

            var url = 'api/TierMeeting/GetTier2Report360Dtl';
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {

                $scope.tier2zonal_name = resp.data.zonal_name;
                $scope.tier2_monthname = resp.data.tier2_monthname;
                $scope.vertical = resp.data.vertical;
                $scope.headRMD_name = resp.data.headRMD_name;
                $scope.txttier2_remarks = resp.data.tier2_remarks;
                $scope.tier2_approval_status = resp.data.tier2_approval_status;
                $scope.tier2_submitteddate = resp.data.created_date;
                $scope.tier2_submittedby = resp.data.created_by;
                $scope.uploaddocument2_list = resp.data.tier2document;
                $scope.tier2approvallog = resp.data.tier2approvallog;
                $scope.tier2_approveddate = resp.data.tier2_approveddate;

                if (resp.data.tier2approvallog == null) {
                    $scope.tier2nohistoryapproval = true;
                }
                else {
                    $scope.tier2historyapproval = true;
                }

            });

            var url = 'api/TierMeeting/GetTier3Report360Dtl';
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {

                $scope.mlrc_date = resp.data.MLRC_date;
                $scope.monthname = resp.data.tier3_month;
                $scope.txttier3_followup = resp.data.follow_up;
                $scope.tier3_status = resp.data.tier3_status;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.uploaddocument3_list = resp.data.tier3document;
                $scope.completed_date = resp.data.completed_date;
                $scope.completed_by = resp.data.completed_by;
                $scope.completed_flag = resp.data.completed_flag;
                $scope.completed_remarks = resp.data.completed_remarks;
                $scope.vertical = resp.data.vertical;

            });
        }
        $scope.closeAllocation = function () {
       
            $scope.ShowAllocation360 = false;
        }

        $scope.gotoback = function () {
            $state.go('app.customerReport');
        }

        $scope.escrowInfoView = function (escrow_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EscrowInfoModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    escrow_gid: escrow_gid
                }
                var url = "api/customerManagement/HistoryEscrowView";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.escrowview = resp.data;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.scheduleLoghistory = function (schedulelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SchedulehistoryModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    schedulelog_gid: schedulelog_gid
                }
                var url = "api/visitReport/GetScheduleLogHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.scheduleList = resp.data.schedulelogdtl;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.visit_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.viewrepayment = function (repyment_gid)
        {
            var modalInstance = $modal.open({
                templateUrl: '/viewrepayment.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    repyment_gid: repyment_gid
                }
                
                var url = "api/MstRepayment/GetRepayment_info";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.account_no = resp.data.account_no;
                    $scope.repayment_date = resp.data.repayment_date;
                    $scope.transaction_date = resp.data.transaction_date;
                    $scope.transaction_id = resp.data.transaction_id;
                    $scope.repayment_amount = resp.data.repayment_amount;
                    $scope.principal = resp.data.principal;
                    $scope.normal_interest = resp.data.normal_interest;
                    $scope.penal_interest = resp.data.penal_interest;
                    $scope.for_feiture_waiver = resp.data.for_feiture_waiver;
                    $scope.user_id = resp.data.user_id;
                    $scope.instrument = resp.data.instrument;
                    $scope.repayment_type = resp.data.repayment_type;
                    $scope.dpd = resp.data.dpd;
                    $scope.old_dpd = resp.data.old_dpd;
                    $scope.account_code = resp.data.account_code;
                    $scope.interest_tds = resp.data.interest_tds;
                    $scope.remarks = resp.data.remarks;
                    $scope.URN = resp.data.URN;
                });

                $scope.ok = function () {
                    $modalInstance.close('account_no');
                };

            }
           
        }

        $scope.viewtelecall = function (telecall_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewtelecall.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    telecall_gid: telecall_gid
                }
               
                var url = "api/MstTelecall/GetTelecall_info";
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                   
                    $scope.account_no = resp.data.account_no;
                    $scope.start_time = resp.data.start_time;
                    $scope.completetion_time = resp.data.completetion_time;
                    $scope.email_address = resp.data.email_address;
                    $scope.name = resp.data.name;
                    $scope.relationship = resp.data.relationship;
                    $scope.customer_name = resp.data.customer_name;
                    $scope.typeof_call = resp.data.typeof_call;
                    $scope.call_details = resp.data.call_details;
                    $scope.spoken_to = resp.data.spoken_to;
                    $scope.natureof_business = resp.data.natureof_business;
                    $scope.reason_OD = resp.data.reason_OD;
                    $scope.telecall_status = resp.data.telecall_status;
                    $scope.courseof_action = resp.data.courseof_action;
                    $scope.ptp_date = resp.data.ptp_date;
                    $scope.ptp_amount = resp.data.ptp_amount;
                    $scope.remarksby_telecaller = resp.data.remarksby_telecaller;
                    $scope.followup_date = resp.data.followup_date;
                    $scope.ledger_balance = resp.data.ledger_balance;
                    $scope.total_demand_due = resp.data.total_demand_due;
                    $scope.URN = resp.data.URN;
                    
                    unlockUI();
                   
                });

                $scope.ok = function () {
                    $modalInstance.close('account_no');
                };

            }

        }

        $scope.viewcibildetails = function (cibildatadtl_gid)
        {
            var modalInstance = $modal.open({
                templateUrl: '/viewCIBILdetails.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cibildatadtl_gid: cibildatadtl_gid
                }
                
                var url = "api/CustomerReport/GetCustomerCibilView";
                SocketService.getparams(url, params).then(function (resp) {
                    console.log(resp);
                    $scope.txtname = resp.data.name;
                    $scope.txtsubmission_type = resp.data.submission_type;
                    $scope.txtsubmitted_on =resp.data.submitted_on;
                    $scope.txtclosed_on =resp.data.closed_on;
                    $scope.txtindicator = resp.data.indicator;
                    $scope.txtaccount_no = resp.data.account_no;
                    $scope.txtcurrent_balance = resp.data.current_balance;
                    $scope.txtoverdue_amount = resp.data.overdue_amount;
                    $scope.txtodbucket_01 = resp.data.odbucket_01;
                    $scope.txtodbucket_02 = resp.data.odbucket_02;
                    $scope.txtodbucket_03 = resp.data.odbucket_03;
                    $scope.txtodbucket_04 = resp.data.odbucket_04;
                    $scope.txtodbucket_05 = resp.data.odbucket_05;
                    $scope.txtod_days = resp.data.od_days;
                    $scope.txtaccount_status = resp.data.account_status;
                    $scope.txtcibil_status = resp.data.cibil;
                    $scope.txthighmark_status = resp.data.highmark;
                    $scope.txtexperian_status = resp.data.experian;
                    $scope.txtequifax_status = resp.data.euifax;
                });

                $scope.ok = function () {
                    $modalInstance.close('account_no');
                };

            }
           
        }

        $scope.downloads = function (val1, val2) {
             DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.dn1historypdf = function (tempdn1format_gid) {


            var params = {
                tempdn1format_gid: tempdn1format_gid

            };
            console.log(params);
            var url = 'api/misDataimport/GetDN1Template_history';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
          
                var phyPath = resp.data;
                //console.log(resp.data);
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //link.download = "Report - DN1";
                //var uri = str;
                //link.href = uri;
                //link.click();
                

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
                        Notify.alert('DN1 Report Downloaded Successfully', 'success')
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export PDF !', 'warning');
                    }
                });
            });

        }
        $scope.dn2historypdf = function (tempdn1format_gid) {


            var params = {
                tempdn1format_gid: tempdn1format_gid

            };
            console.log(params);
            var url = 'api/misDataimport/GetDN2Template_history';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
             
                var phyPath = resp.data;
                //console.log(resp.data);
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //link.download = "Report - DN1";
                //var uri = str;
                //link.href = uri;
                //link.click();
               

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
                        Notify.alert('DN2 Report Downloaded Successfully', 'success')
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export PDF !', 'warning');
                    }
                });
            });

        }
        $scope.dn3historypdf = function (tempdn1format_gid) {


            var params = {
                tempdn1format_gid: tempdn1format_gid

            };
            console.log(params);
            var url = 'api/misDataimport/GetDN3Template_history';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
           
                var phyPath = resp.data;
                //console.log(resp.data);
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //link.download = "Report - DN1";
                //var uri = str;
                //link.href = uri;
                //link.click();
                

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
                        Notify.alert('DN3 Report Downloaded Successfully', 'success')
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export PDF !', 'warning');
                    }
                });
            });

        }
        $scope.dn1pdf = function () {


            var params = {
                urn: $scope.urn

            };
            console.log(params);
            var url = 'api/misDataimport/DN1pdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                var phyPath = resp.data;
                console.log(resp.data);
                var relPath = phyPath.split("EMS");
                var relpath1 = relPath[1].replace("\\", "/");
                var hosts = window.location.host;
                var prefix = location.protocol + "//";
                var str = prefix.concat(hosts, relpath1);
                var link = document.createElement("a");
                link.download = "Report - DN1";
                var uri = str;
                link.href = uri;
                link.click();
                Notify.alert('DN1 Report Downloaded Successfully', 'success')
                unlockUI();
            });

        }

        $scope.dn2pdf = function () {

            var params = {
                urn: $scope.urn

            };
            console.log(params);
            var url = 'api/misDataimport/DN2pdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                var phyPath = resp.data;
                var relPath = phyPath.split("EMS");
                var relpath1 = relPath[1].replace("\\", "/");
                var hosts = window.location.host;
                var prefix = location.protocol + "//";
                var str = prefix.concat(hosts, relpath1);
                var link = document.createElement("a");
                link.download = "Report - DN2";
                var uri = str;
                link.href = uri;
                link.click();
                Notify.alert('DN2 Report Downloaded Successfully', 'success')
                unlockUI();
            });

        }
        $scope.dn3pdf = function () {

            var params = {
                urn: $scope.urn

            };
           
            var url = 'api/misDataimport/DN3pdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                var phyPath = resp.data;
                var relPath = phyPath.split("EMS");
                var relpath1 = relPath[1].replace("\\", "/");
                var hosts = window.location.host;
                var prefix = location.protocol + "//";
                var str = prefix.concat(hosts, relpath1);
                var link = document.createElement("a");
                link.download = "Report - DN3";
                var uri = str;
                link.href = uri;
                link.click();
                Notify.alert('DN3 Report Downloaded Successfully', 'success')
                unlockUI();
            });

        }
        
    }
    })();

