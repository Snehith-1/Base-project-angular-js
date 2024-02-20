(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionEditController', MstSanctionEditController);

    MstSanctionEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstSanctionEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionEditController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.application2sanction_gid = $location.search().application2sanction_gid;
        var application2sanction_gid = $scope.application2sanction_gid;
        var lspage = $scope.lspage;
        $scope.lsresubmit = $location.search().lsresubmit;
        var lsresubmit = $scope.lsresubmit;

        var vertical_gid;
        var vertical_code;
        activate();
        lockUI();
        function activate() {

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

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/MstCADApplication/GetApplicationBasicView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();

                $scope.txtcustomer_urn = resp.data.customer_urn;

            });
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCadFlow/GetBuyerList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.rmbuyer_list = resp.data.rmbuyer_list;
                $scope.creditbuyer_list = resp.data.creditbuyer_list;
            });

            var url = 'api/MstCAD/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtentity_gid = resp.data.entity_gid;
                $scope.txtentity_name = resp.data.entity_name;
                $scope.txtcreditapproved_date = resp.data.creditapproved_date;
                $scope.txtvertical_code = resp.data.vertical_code;
            });

            // Get CC Member List
            var url = 'api/MstCC/GetScheduleMeeting';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ccmember_list = resp.data.ccmember_list;
                $scope.otheruser_list = resp.data.otheruser_list;
            });
            //Facility Type Drop Down
            var url = 'api/MstCadFlow/GetProductList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loanfacility_list = resp.data.producttype_list;
            });

            //Sanction Type Drop Down
            var url = 'api/MstCadFlow/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiontype_list = resp.data.sanctiontype_list;
            });

            // Get Primary Mobile No, Mail ID
            var url = 'api/MstCADApplication/GetMobileMailDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimary_mobileno = resp.data.primary_mobileno;
                $scope.txtprimary_email = resp.data.primary_email;
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
            // Get Overall Limit
            var url = 'api/MstCadFlow/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtSanctionAmount = resp.data.overalllimit_amount;
            });
            // Get Credit Approval Hirerichy
            var url = 'api/MstCadFlow/Getcreditheadsview';
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
            // Get MOM and CAM Document
            var url = 'api/MstCAD/GetMOMCAMDocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.momuploaddocument_list = resp.data.cadmomdocument_list;
                $scope.camuploaddocument_list = resp.data.cadcamdocument_list;
            });
            var param = {
                application_gid: application_gid,
                employee_gid: employee_gid,
            }
            // Get RM Approval Hirerichy
            var url = 'api/MstCadFlow/Getapplicationhierarchylist';

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
            var url = 'api/MstCADApplication/GetProductChargesDtl';
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

            var url = 'api/MstCAD/GetSanctionEdit';
            var params = {
                application2sanction_gid: application2sanction_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionto_list = resp.data.sanctionto_list;
                $scope.cadaddress_list = resp.data.cadaddress_list;
                $scope.cadmobileno_list = resp.data.cadmobileno_list;
                $scope.cademail_list = resp.data.cademail_list;
                $scope.application2sanction_gid = resp.data.application2sanction_gid;
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.txtSanctionDateEdit = resp.data.sanctionDate;
                $scope.SanctionAmountEdit = resp.data.sanction_amount;
                $scope.entity = resp.data.entity;
                $scope.entity_gid = resp.data.entity_gid;
                $scope.application_gid = resp.data.application_gid;
                $scope.cbosanction_type = resp.data.applicationtype_gid;
                $scope.rdbpaycard = resp.data.paycard;
                $scope.cbobranch_gid = resp.data.branch_gid;
                $scope.rdbdeclaration = resp.data.esdeclaration_status;
                if (resp.data.esdeclaration_status == 'Yes') {
                    $scope.esdeclarationyes = true
                    $scope.esdeclarationno = false;
                    refreshESdocumentdetails();
                }
                if (resp.data.esdeclaration_status == 'No') {
                    $scope.esdeclarationyes = false
                    $scope.esdeclarationno = true
                    refreshDeviationMaildocumentdetails();
                }
                $scope.sanctiontype_name = resp.data.application_type;
                $scope.cbosanctionto = resp.data.sanctionto_gid;
                $scope.sanctionto_name = resp.data.sanctionto_name;
                $scope.cboaddress = resp.data.contactpersonaddress_gid;
                $scope.contactperson_address = resp.data.contactperson_address;
                $scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.contactperson_number = resp.data.contactperson_number;
                $scope.cbomobileno = resp.data.contactpersonmobileno_gid;
                $scope.cboemail = resp.data.contactpersonemail_gid;
                $scope.contactpersonemail_address = resp.data.contactpersonemail_address;
                $scope.rdbsanction_type = resp.data.sanction_type;
                $scope.cbonature_proposal = resp.data.natureof_proposal;
                $scope.txtSanctionFromDateEdit = resp.data.sanctionfromDate;
                $scope.txtSanctionTillDateEdit = resp.data.sanctiontillDate;
                if (resp.data.sanction_type == 'Existing Customer') {
                    $scope.existing_customer = true;
                }
                else {
                    $scope.existing_customer = false;
                }
            });
            var url = 'api/MstLSA/lsabranch';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.branch_list = resp.data.branch_list;
            });
            refreshproductdetails();
        }

        function refreshproductdetails() {
            lockUI();
            var params = {
                application_gid: $scope.application_gid,
                application2sanction_gid: application2sanction_gid
            }
            var url = 'api/MstCAD/GetApp2SanctionLimitInfoDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.productdetails = resp.data.limitandproducts;
            });
        }

        function refreshESdocumentdetails() {
            lockUI();
            var params = {
                application2sanction_gid: application2sanction_gid
            }
            var url = 'api/MstCAD/GetApp2sanctionesdocument';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.uploadesfilename_list = resp.data.UploadCADES_DocumentList;
            });
        }

        function refreshDeviationMaildocumentdetails() {
            lockUI();
            var params = {
                application2sanction_gid: application2sanction_gid
            }
            var url = 'api/MstCAD/GetApp2sanctionMaildocument';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mailfilename_list = resp.data.DeviationCADMail_DocumentList;
            });
        }

        $scope.rdbdeclaration_yes = function () {
            $scope.esdeclarationyes = true;
            $scope.esdeclarationno = false;
            refreshESdocumentdetails();
        }
        $scope.rdbdeclaration_no = function () {
            $scope.esdeclarationyes = false;
            $scope.esdeclarationno = true;
            refreshDeviationMaildocumentdetails();
        }

        $scope.edit_product = function (limitproductinfodtl_gid, loandtls_list, txtSanctionAmount, DBdocumented_limit) {
            var totaldocumentlimitamount = '0';
            var params = {
                application_gid: application_gid,
                application2sanction_gid: application2sanction_gid
            }
            var url = 'api/MstCAD/GetSanctionlimitvalidation';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                totaldocumentlimitamount = resp.data.total_documentlimit;
            });
            var modalInstance = $modal.open({
                templateUrl: '/productedit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.panel1 = true;
                if (loandtls_list && loandtls_list.length > 1) {
                    $scope.reportstructure_list = angular.copy(loandtls_list);
                    var array1 = [];
                    array1 = {
                        application2loan_gid: 'ODLIM',
                        product_type: 'ODLIM',
                    }
                    $scope.reportstructure_list.push(array1);
                }
                else {
                    $scope.reportstructure_list = [];
                    var array1 = [];
                    array1 = {
                        application2loan_gid: 'ODLIM',
                        product_type: 'ODLIM',
                    }
                    $scope.reportstructure_list.push(array1);
                }
                $scope.interchangeability_yes = function () {
                    $scope.interchangeabilityno = false;
                    $scope.interchangeabilityyes = true;
                    $scope.mandatoryfields = false;
                }

                $scope.interchangeability_no = function () {
                    $scope.interchangeabilityno = true;
                    $scope.interchangeabilityyes = false;
                    $scope.mandatoryfields = false;
                }

                var param = {
                    limitproductinfodtl_gid: limitproductinfodtl_gid,
                }
                var url = 'api/MstLSA/GetLimitProductInfoDtl';
                SocketService.getparams(url, param).then(function (resp) {
                    unlockUI();
                    $scope.rdbinterchangeability = resp.data.interchangeability;
                    $scope.txtdocument_limit = resp.data.documented_limit;
                    //$scope.txtsampledocument_limit = resp.data.documented_limit;
                    $scope.txtexpiry_date = resp.data.dateofExpiry;
                    if ($scope.txtexpiry_date != "") {
                        $scope.txtexpiry_date = new Date($scope.txtexpiry_date);
                    }
                    if ($scope.txtdocument_limit != null && $scope.txtdocument_limit != undefined && $scope.txtdocument_limit != "") {
                        var str = $scope.txtdocument_limit.replace(/,/g, '');
                        var str = str.split('.')[0];
                        $scope.txtdocument_limit = Number(str).toLocaleString('en-IN');
                        document.getElementById('documentlimitamount_words').innerHTML = cmnfunctionService.fnConvertNumbertoWord(str);

                        //$scope.txtdocument_limit = (parseInt($scope.txtsampledocument_limit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');

                        //$scope.lblamountseperator = (parseInt($scope.txtdocument_limit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        //$scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                        //document.getElementById('words_totalamount7').innerHTML = $scope.lblamountwords;
                    }
                    if (resp.data.interchangeability == 'Yes') {
                        $scope.interchangeabilityno = false;
                        $scope.interchangeabilityyes = true;
                        $scope.mandatoryfields = false;
                        $scope.$parent.cboreport_structure = resp.data.report_structuregid;
                    } else {
                        $scope.interchangeabilityno = true;
                        $scope.interchangeabilityyes = false;
                        $scope.mandatoryfields = false;
                        $scope.$parent.cboapplicable_condition = resp.data.odlim_condition;
                    }
                });

                $scope.Submit_productupdate = function () {
                    var report_structuregid = "", report_structure = "";
                    if ($scope.rdbinterchangeability == "Yes") {
                        report_structuregid = $scope.cboreport_structure;
                        report_structure = $('#report_structure :selected').text();
                        $scope.cboapplicable_condition = "";
                    }
                    var params = {
                        limitproductinfodtl_gid: limitproductinfodtl_gid,
                        interchangeability: $scope.rdbinterchangeability,
                        report_structuregid: report_structuregid,
                        report_structure: report_structure,
                        odlim_condition: $scope.cboapplicable_condition,
                        dateof_Expiry: $scope.txtexpiry_date,
                        documented_limit: $scope.txtdocument_limit,
                    }
                    lockUI();
                    var url = 'api/MstCAD/SanctionUpdatelimitproduct';
                    SocketService.post(url, params).then(function (resp) {
                        $modalInstance.close('closed');
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            refreshproductdetails();
                        }
                        else {
                            refreshproductdetails();
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }

                $scope.documentlimitchange = function () {
                    var input = document.getElementById('txtInput1').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');
                    var lswords_documentlimit = cmnfunctionService.fnConvertNumbertoWord(str);
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    if (output == 'NaN') {
                        Notify.alert('Accept Numeric Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtdocument_limit = "";

                    }
                    else {
                        document.getElementById('documentlimitamount_words').innerHTML = lswords_documentlimit;
                        $scope.txtdocument_limit = amount;
                        var diffdocument_limit = 0;
                        if ($scope.rdbinterchangeability == 'No' && $scope.cboapplicable_condition == 'Applicable') {
                            diffdocument_limit = (totaldocumentlimitamount.replace(/[\s,]+/g, '').trim()) - (DBdocumented_limit.replace(/[\s,]+/g, '').trim());
                            diffdocument_limit = Math.abs(diffdocument_limit);
                        }
                        if (((txtSanctionAmount.replace(/[\s,]+/g, '').trim()) - (diffdocument_limit)) < $scope.txtdocument_limit.replace(/[\s,]+/g, '').trim()) {
                            $scope.panel1 = false;
                        }
                        else {
                            $scope.panel1 = true;
                        }
                    }
                    $scope.mandatoryfields = false;
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.uploades_declaration = function (val, val1, name) {
            if (($scope.es_declarationdocument_type == null) || ($scope.es_declarationdocument_type == '') || ($scope.es_declarationdocument_type == undefined)) {
                $("#adduploades_declaration").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                //var item = {
                //    name: val[0].name,
                //    file: val[0]
                //};
                //var frm = new FormData();
                //frm.append('fileupload', item.file);
                //frm.append('file_name', item.name);

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
                frm.append('document_type', $scope.es_declarationdocument_type)
                frm.append('application2sanction_gid', application2sanction_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;

                var url = 'api/MstCAD/Uploades_declarationdocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    if (resp.data.status == true) {
                        $("#adduploades_declaration").val('');
                        unlockUI();
                        $scope.es_declarationdocument_type = '';

                        var params = {
                            application2sanction_gid: application2sanction_gid
                        }
                        var url = 'api/MstCAD/GetApp2sanctionesdocument';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.uploadesfilename_list = resp.data.UploadCADES_DocumentList;
                        });

                        Notify.alert(resp.data.message, 'success')
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }
                });
            }
        }

        $scope.esdocument_cancelclick = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/MstCAD/uploadesdocumentadd_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.uploadesfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.uploadesfilename_list.splice(key, 1);
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
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }


        //Upload Deviation Mail Document
        $scope.deviationmailupload = function (val, val1, name) {
            if (($scope.deviationmaildocument_type == null) || ($scope.deviationmaildocument_type == '') || ($scope.deviationmaildocument_type == undefined)) {
                $("#addmailupload").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                //var item = {
                //    name: val[0].name,
                //    file: val[0]
                //};
                //var frm = new FormData();
                //frm.append('fileupload', item.file);
                //frm.append('file_name', item.name);

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
                frm.append('document_type', $scope.deviationmaildocument_type)
                frm.append('application2sanction_gid', application2sanction_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;

                var url = 'api/MstCAD/Uploadmaildocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    if (resp.data.status == true) {
                        $("#addmailupload").val('');
                        $scope.deviationmaildocument_type = '';
                        unlockUI();
                        var params = {
                            application2sanction_gid: application2sanction_gid
                        }
                        var url = 'api/MstCAD/GetApp2sanctionMaildocument';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.mailfilename_list = resp.data.DeviationCADMail_DocumentList;
                        });
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }

                });
            }

        }

        $scope.maildocument_cancelclick = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/MstCAD/Maildocumentadd_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.mailfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.mailfilename_list.splice(key, 1);
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
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.sanctiontype_change = function (cbosanction_type) {
            var lssanctiontype_name = $('#sanctiontype_name :selected').text();
            var params = {
                sanctiontype_name: lssanctiontype_name,
                application_gid: application_gid,
            }
            var url = 'api/MstCadFlow/GetSanctionToList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionto_list = resp.data.sanctionto_list;
            });
        }
        $scope.sanctionto_change = function (cbosanctionto) {
            var params = {
                sanctionto_gid: $scope.cbosanctionto,
            }
            var url = 'api/MstCadFlow/GetContactPersonDetail';
            SocketService.getparams(url, params).then(function (resp) {
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

        $scope.downloadsCAM = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadsMOM = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
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

        // Numeric to Word - Indian Standard...//
        

        $scope.sanctiontype_existing = function () {
            $scope.existing_customer = true;
        }

        $scope.sanctiontype_new = function () {
            $scope.existing_customer = false;
        }

        $scope.interchangeability_yes = function () {
            $scope.interchangeabilityno = false;
            $scope.interchangeabilityyes = true;
            $scope.mandatoryfields = false;
        }
        $scope.interchangeability_no = function () {
            $scope.interchangeabilityno = true;
            $scope.interchangeabilityyes = false;
            $scope.mandatoryfields = false;
        }

        $scope.onselectedchangeccgroup = function () {
            var params = {
                ccgroupname_gid: $scope.cboccgroup_name.ccgroupname_gid
            }
            var url = 'api/MstCCMember/Getccgroup2member';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ccmember_list = resp.data.ccmember_list;
            });
        }

        $scope.changevaliditymnt = function (txtvalidity_monthsedit) {
            if ($scope.txtvalidity_monthsedit == undefined) {
                $scope.validitymonth = true;
            }
            else {
                $scope.validitymonth = false;
            }
        }

        $scope.onchangemandaotory = function () {
            $scope.mandatoryfields = false;
        }

        $scope.Back = function (val) {
            if (lspage == 'SanctionApprovalCompleted') {
                $location.url('app/MstSanctionDtlViewSummary?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lsresubmit=rewubmit_flag');
            }
            else {
                $location.url('app/MstSanctionDtlSummary?application_gid=' + application_gid + '&lsemployee_gid=' + employee_gid);
            }              
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
                var url = 'api/MstCC/ViewCCRemarks';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblremarks = resp.data.remarks;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }
        //$scope.other_remarksview = function (ccmeeting2othermembers_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/viewccremarks.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        var params =
        //           {
        //               ccmeeting2othermembers_gid: ccmeeting2othermembers_gid,
        //               application_gid: application_gid,
        //           }
        //        var url = 'api/MstCC/ViewOtherRemarks';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.lblremarks = resp.data.remarks;
        //        });
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //    }
        //}

        $scope.add_product = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/productadd.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.interchangeability_yes = function () {
                    $scope.interchangeabilityno = false;
                    $scope.interchangeabilityyes = true;
                    $scope.mandatoryfields = false;
                }

                $scope.interchangeability_no = function () {
                    $scope.interchangeabilityno = true;
                    $scope.interchangeabilityyes = false;
                    $scope.mandatoryfields = false;
                }

                var param = {
                    application2loan_gid: application2loan_gid,
                }
                var url = 'api/MstCadFlow/GetLoanDetail';
                SocketService.getparams(url, param).then(function (resp) {
                    unlockUI();
                    $scope.rdbinterchangeability = resp.data.interchangeability;
                    //$scope.txtdocument_limit = resp.data.document_limit;


                    $scope.txtsampledocument_limit = resp.data.document_limit;
                    $scope.txtdocument_limit = (parseInt($scope.txtsampledocument_limit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');

                    $scope.lblamountseperator = (parseInt($scope.txtdocument_limit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount7').innerHTML = $scope.lblamountwords;

                    $scope.txtexpiry_date = new Date(resp.data.expiry_date);
                    if (resp.data.interchangeability == 'Yes') {
                        $scope.interchangeabilityno = false;
                        $scope.interchangeabilityyes = true;
                        $scope.mandatoryfields = false;
                        $scope.cboreport_structure = resp.data.report_structure;
                    } else {
                        $scope.interchangeabilityno = true;
                        $scope.interchangeabilityyes = false;
                        $scope.mandatoryfields = false;
                        $scope.cboapplicable_condition = resp.data.report_structure;
                    }
                });

                $scope.Submit_product = function () {
                    var params = {
                        application_gid: application_gid,
                        application2loan_gid: application2loan_gid,
                        interchangeability: $scope.rdbinterchangeability,
                        report_structure: $scope.cboreport_structure,
                        applicable_condition: $scope.cboapplicable_condition,
                        documentlimitchange: $scope.documentlimitchange,
                        expiry_date: $scope.txtexpiry_date,
                        document_limit: $scope.txtdocument_limit
                    }
                    lockUI();
                    var url = 'api/MstCAD/UpdateLoanDetails';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }

                function defaultamountwordschange(input) {
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
                    return lswords;
                }


               


                $scope.documentlimitchange = function () {
                    var input = document.getElementById('txtInput1').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');
                    var lswords_documentlimit = cmnfunctionService.fnConvertNumbertoWord(str);
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    if (output == 'NaN') {
                        Notify.alert('Accept Numeric Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtdocument_limit = "";

                    }
                    else {
                        document.getElementById('documentlimitamount_words').innerHTML = lswords_documentlimit;
                        $scope.txtdocument_limit = amount;

                        if ((($scope.txtSanctionAmount.replace(/[\s,]+/g, '').trim()) - ($scope.totaldocumentlimitamount.replace(/[\s,]+/g, '').trim())) < ($scope.txtdocument_limit.replace(/[\s,]+/g, '').trim())) {
                            $scope.panel1 = false;
                        }
                        else {
                            $scope.panel1 = true;
                        }
                    }
                    $scope.mandatoryfields = false;
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //Sanction Submit Event
        $scope.sanctionUpdate = function () {
            var lssanctiontype_name = $('#sanctiontype_name :selected').text();
            var lssanctionto_name = $('#sanctionto_name :selected').text();
            var lsaddress = $('#address :selected').text();
            var lsmobile_no = $('#mobile_no :selected').text();
            var lsemail_address = $('#email_address :selected').text();
            var lsbranch_name = $('#branch_name :selected').text();
            var params = {
                application2sanction_gid: application2sanction_gid,
                sanction_date: $scope.txtSanctionDateEdit,
                sanction_type: $scope.rdbsanction_type,
                natureof_proposal: $scope.cbonature_proposal,
                applicationtype_gid: $scope.cbosanction_type,
                application_type: lssanctiontype_name,
                sanctionto_gid: $scope.cbosanctionto,
                sanctionto_name: lssanctionto_name,
                sanctionfrom_date: $scope.txtSanctionFromDateEdit,
                sanctiontill_date: $scope.txtSanctionTillDateEdit,
                contactpersonaddress_gid: $scope.cboaddress,
                contactperson_address: lsaddress,
                contactperson_name: $scope.txtcontactperson_name,
                contactperson_number: lsmobile_no,
                contactpersonmobileno_gid: $scope.cbomobileno,
                contactpersonemail_gid: $scope.cboemail,
                contactpersonemail_address: lsemail_address,
                application_gid: application_gid,
                paycard: $scope.rdbpaycard,
                esdeclaration_status: $scope.rdbdeclaration,
                branch_gid: $scope.cbobranch_gid,
                branch_name: lsbranch_name
            }

            var url = 'api/MstCAD/PostUpdateSanction';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')

                    if (lspage == 'SanctionApprovalCompleted') {
                        $location.url('app/MstSanctionDtlViewSummary?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage );
                    }
                    else {
                        $location.url('app/MstSanctionDtlSummary?application_gid=' + application_gid + '&lsemployee_gid=' + employee_gid);
                    }  
                    
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });
        }

        $scope.sanctionReUpdate = function () {
            var lssanctiontype_name = $('#sanctiontype_name :selected').text();
            var lssanctionto_name = $('#sanctionto_name :selected').text();
            var lsaddress = $('#address :selected').text();
            var lsmobile_no = $('#mobile_no :selected').text();
            var lsemail_address = $('#email_address :selected').text();
            var lsbranch_name = $('#branch_name :selected').text();
            var params = {
                application2sanction_gid: application2sanction_gid,
                sanction_date: $scope.txtSanctionDateEdit,
                sanction_type: $scope.rdbsanction_type,
                natureof_proposal: $scope.cbonature_proposal,
                applicationtype_gid: $scope.cbosanction_type,
                application_type: lssanctiontype_name,
                sanctionto_gid: $scope.cbosanctionto,
                sanctionto_name: lssanctionto_name,
                sanctionfrom_date: $scope.txtSanctionFromDateEdit,
                sanctiontill_date: $scope.txtSanctionTillDateEdit,
                contactpersonaddress_gid: $scope.cboaddress,
                contactperson_address: lsaddress,
                contactperson_name: $scope.txtcontactperson_name,
                contactperson_number: lsmobile_no,
                contactpersonmobileno_gid: $scope.cbomobileno,
                contactpersonemail_gid: $scope.cboemail,
                contactpersonemail_address: lsemail_address,
                application_gid: application_gid,
                paycard: $scope.rdbpaycard,
                esdeclaration_status: $scope.rdbdeclaration,
                branch_gid: $scope.cbobranch_gid,
                branch_name: lsbranch_name
            }

            var url = 'api/MstCAD/PostReUpdateSanction';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')

                    if (lspage == 'SanctionApprovalCompleted') {
                        $location.url('app/MstSanctionDtlViewSummary?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lsresubmit=rewubmit_flag');
                    }
                    else {
                        $location.url('app/MstSanctionDtlSummary?application_gid=' + application_gid + '&lsemployee_gid=' + employee_gid);
                    }

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });
        }

        $scope.sanctionsubmit = function () {
         
            var params = {
                application2sanction_gid: application2sanction_gid,              
                application_gid: application_gid
               
            }

            var url = 'api/MstCAD/SanctionSubmitToApproval';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')

                    if (lspage == 'SanctionApprovalCompleted') {
                        $location.url('app/MstSanctionDtlViewSummary?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else {
                        $location.url('app/MstSanctionDtlSummary?application_gid=' + application_gid + '&lsemployee_gid=' + employee_gid);
                    }

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });
        }



        $scope.PurposeofLoanOther_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoanOther.html',
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
                var url = 'api/MstApplicationView/GetPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });
                var url = 'api/MstApplicationView/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
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
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       application2loan_gid: application2loan_gid
                   }
                var url = 'api/MstCADApplication/GetCollateralDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Collateraldoc_list = resp.data.CollatralDocumentList;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_Collateraldoc = function (val1, val2,val3) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    if (val3=='N')
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    else
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                }
                $scope.documentviewer = function (val1, val2,val3) {
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
                    if (val3 == 'N')
                        DownloaddocumentService.DocumentViewer(val1, val2);
                    else
                        DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
                }
                $scope.downloadallcollateral = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        if ($scope.Collateraldoc_list[i].migration_flag == 'N')
                            DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
                        else
                            DownloaddocumentService.OtherDownloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name, $scope.Collateraldoc_list[i].migration_flag);
                    }
                }

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
})();
