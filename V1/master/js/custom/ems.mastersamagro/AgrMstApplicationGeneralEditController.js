
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApplicationGeneralEditController', AgrMstApplicationGeneralEditController);

    AgrMstApplicationGeneralEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AgrMstApplicationGeneralEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApplicationGeneralEditController';

        lockUI();
        activate();
        function activate() {
            $scope.amendmentshow = false;
            $scope.application_gid = localStorage.getItem("application_gid");
            var application_gid = $scope.application_gid;
          
            $scope.amount_validation = true;           
        
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                unlockUI();
            });
         
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblbasiccustomer_name = resp.data.customer_name;
                $scope.lblamendment_remarks = resp.data.amendment_remarks;
                unlockUI();

                if ($scope.lblamendment_remarks == null || $scope.lblamendment_remarks == '' || $scope.lblamendment_remarks == undefined) {
                    $scope.amendmentshow = false;
                }
                else {
                    $scope.amendmentshow = true;
                }
            });

            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && $scope.approveinitiated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                    $scope.resubmitshow = false;
                }
                else if ($scope.proceed_flag == 'Y' && $scope.approveinitiated_flag == 'N' && $scope.hierarchyupdated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_enable = true;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'Y' && approveinitiated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = true;
                    $scope.done_enable = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N' && approveinitiated_flag == 'N') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_disable = true;
                    $scope.resubmitshow = false;
                }
                else {

                }
            });

            var url = 'api/AgrMstApplicationAdd/GetIndividualTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/AgrMstApplicationAdd/GetIntitutionTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrMstApplicationEdit/GetBasicDetailsSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.basicdetails_list = resp.data.basicdetails_list;
                $scope.application_status = resp.data.basicdetails_list[0].application_status;
            });
            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrTrnApplicationApproval/Getproceedapprovalflag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.proceedtoapproval_flag = resp.data.proceedtoapproval_flag;
               
            });


            var url = 'api/AgrMstApplicationEdit/GetIndividualSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.contact_list = resp.data.contact_list;
            });

            var url = 'api/AgrMstApplicationAdd/GetInstitutionEditSummary';
            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
                $scope.institution_list = resp.data.institution_list;
            });

            var url = 'api/AgrMstApplicationEdit/GetEditProductcharges';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                $scope.lblprocessing_fee = resp.data.processing_fee;
                $scope.lbldoc_charges = resp.data.doc_charges;
                $scope.application_gid = resp.data.application_gid;
                $scope.applicant_type = resp.data.applicant_type;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.lblcreated_date = resp.data.created_date;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.productcharge_flag = resp.data.productcharge_flag;
                $scope.economical_flag = resp.data.economical_flag;
                $scope.lblproductcharges_status = resp.data.productcharges_status;
                $scope.application_status = resp.data.application_status;
                $scope.hypothecation_flag = resp.data.hypothecation_flag;
                console.log(resp.data.created_date)
                if ($scope.applicant_type == "" || $scope.applicant_type == null) {
                    $scope.applicant_typenull = true;
                    $scope.applicant_typenotnull = false;
                }
                else {
                    $scope.applicant_typenotnull = true;
                    $scope.applicant_typenull = false;
                }
                if ($scope.hypothecation_flag == 'Y') {
                    $scope.hypothecation_tab = true;
                }
                else {
                    $scope.hypothecation_tab = false;
                }
                if ($scope.economical_flag == 'N') {
                    $scope.social_tradetab = false;
                    $scope.social_trade = true;
                }
                else {
                    $scope.social_tradetab = true;
                    $scope.social_trade = false;
                }

                if ($scope.productcharge_flag == 'N') {
                    $scope.product_chargetab = false;
                    $scope.product_charge = true;
                }
                else {
                    $scope.product_chargetab = true;
                    $scope.product_charge = false;
                }
            });
            var url = 'api/AgrMstApplicationEdit/GetEditHypothecation';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.security_type = resp.data.security_type;
                $scope.security_value = resp.data.security_value;
                $scope.securityassessed_date = resp.data.securityassessed_date;
                $scope.asset_id = resp.data.asset_id;
                $scope.created_by = resp.data.created_by;
                $scope.created_date = resp.data.created_date;
            });
            var url = 'api/AgrMstApplicationEdit/GetSocialTradeSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.applicationlist = resp.data.applicationlist;
            });

            var url = 'api/AgrMstApplicationEdit/GetCICEditIndividualSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cicindividuallist = resp.data.cicindividual_list;
            });

            var url = 'api/AgrMstApplicationEdit/GetCICEditInstitutionSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cicinstitutionlist = resp.data.cicinstitution_list;
            });

            var urls = 'api/AgrMstApplicationAdd/CICUploadIndividualDocTempList';
            lockUI();
            SocketService.get(urls).then(function (resp) {
                unlockUI();
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });
            var url = 'api/AgrMstApplicationEdit/GetProductChargesTempClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/AgrMstApplicationAdd/GetproductDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.loanproductlist = resp.data.loanproductlist;
                $scope.loantypelist = resp.data.loantypelist;
                $scope.principalfrequencylist = resp.data.principalfrequencylist;
                $scope.interestfrequencylist = resp.data.interestfrequencylist;
                $scope.buyerlist = resp.data.buyerlist;
                $scope.securitytype_list = resp.data.securitytype_list;
            });

            var url = 'api/AgrMstApplicationEdit/GetGroupSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.group_list = resp.data.group_list;
            });

        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        $scope.doneclick = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                unlockUI();                
            });

            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && $scope.approveinitiated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                    $scope.resubmitshow = false;
                }              
                else if ($scope.proceed_flag == 'Y' && $scope.approveinitiated_flag == 'N' && $scope.hierarchyupdated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_enable = true;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'Y' && approveinitiated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = true;
                    $scope.done_enable = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N' && approveinitiated_flag == 'N') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_disable = true;
                    $scope.resubmitshow = false;
                }
                else {

                }
            });

        }


        $scope.overallsubmit_application = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditAppProceed';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
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
                $state.go('app.AgrMstApplicationCreationSummary');
            });

        }

        $scope.submit = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditAppReProceed';
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
                $state.go('app.AgrMstApplicationCreationSummary');
            });

        }

        $scope.Back = function () {
            $state.go('app.AgrMstApplicationCreationSummary');
        }



        $scope.edit_basicdetails = function (application_gid, application_status, product_gid, variety_gid, renewal_flag, amendment_flag) {           
            $location.url('app/AgrMstApplcreationBasicdtlEdit?lsapplication_gid=' + application_gid + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid + '&lstab=edit&lsstatus=' + application_status + '&renewal_flag=' + renewal_flag + '&amendment_flag=' + amendment_flag);
        }

        $scope.edit_individual = function (contact_gid) {
            $location.url('app/AgrMstApplcreationIndividualdtlEdit?lscontact_gid=' + contact_gid + '&lstab=edit');
        }

        $scope.edit_institution = function (institution_gid) {
            $location.url('app/AgrMstApplcreationInstitutiondtlEdit?lsinstitution_gid=' + institution_gid + '&lstab=edit');
        }

        $scope.edit_group = function (group_gid) {
            $location.url('app/AgrMstApplcreationGroupdtlEdit?lsgroup_gid=' + group_gid + '&lstab=edit');
        }

        $scope.edit_socialtrade = function (application_gid) {
            $location.url('app/AgrMstApplcreationSocialTradeEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
        }

        $scope.edit_productcharges = function (application_gid) {
            $location.url('app/AgrMstApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
        }

        $scope.edit_cicupload = function () {
            $state.go('app.AgrMstApplcreationCICUploadEdit');
        }

        $scope.cicupload_individualEdit = function (contact_gid) {
            $location.url('app/AgrMstApplcreationCICUploadEdit?lscontact_gid=' + contact_gid + '');
        }

        $scope.cicupload_institutionEdit = function (institution_gid) {
            $location.url('app/AgrMstApplcreationCICUploadInstEdit?lsinstitution_gid=' + institution_gid + '');
        }

        $scope.edit_group = function (group_gid) {
            $location.url('app/AgrMstApplcreationGroupdtlEdit?lsgroup_gid=' + group_gid + '&lstab=edit');
        }

        $scope.bureauupdate_individual = function (contact_gid, application_gid) {
            $location.url('app/AgrMstBureauUpdateIndividual?lsapplication_gid=' + application_gid + '&lscontact_gid=' + contact_gid);
        }

        $scope.bureauupdate_institution = function (institution_gid, application_gid) {
            $location.url('app/AgrMstBureauUpdateInstitution?lsapplication_gid=' + application_gid + '&lsinstitution_gid=' + institution_gid);
        }

        //Institution
        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            var url = 'api/AgrMstbuyer/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyearin_business = resp.data.year_business;
                $scope.txtmonthsin_business = resp.data.month_business;
            });
        }

        $scope.InstitutionForm60DocumentUpload = function (val, val1, name) {

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
            frm.append('fileupload', item.file);

            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/AgrMstApplicationAdd/InstitutionForm_60DocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.institutionform60upload_list = resp.data.institutionupload_list;
                    unlockUI();

                    $("#institutionform60file").val('');
                    $scope.txtinstitutionform60_document = "";
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

        $scope.institutionuploaddocumentcancel = function (institution2form60documentupload_gid) {
            lockUI();
            var params = {
                institution2form60documentupload_gid: institution2form60documentupload_gid
            }
            var url = 'api/AgrMstApplicationAdd/InstitutionForm_60DocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionform60upload_list = resp.data.institutionupload_list;
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

        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.save_institution = function () {
            if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
                Notify.alert('Kindly select Stakeholder Type', 'warning')
            }
            else {


                var lscompanytype_gid = '';
                var lscompanytype_name = '';
                var lsusertype_gid = '';
                var lsuser_type = '';
                var lsassessmentagency_gid = '';
                var lsassessmentagency_name = '';
                var lsassessmentagencyrating_gid = '';
                var lsassessmentagencyrating_name = '';
                var lsbusinesscategory_gid = '';
                var lsbusinesscategory_name = '';
                var lsdesignation_gid = '';
                var lsdesignation_type = '';
                var lsamlcategory_gid = '';
                var lsamlcategory_name = '';
                if ($scope.cboCompanytype != undefined || $scope.cboCompanytype != null) {
                    lscompanytype_gid = $scope.cboCompanytype.companytype_gid;
                    lscompanytype_name = $scope.cboCompanytype.companytype_name;
                }
                if ($scope.cboStakeholdertype != undefined || $scope.cboStakeholdertype != null) {
                    lsusertype_gid = $scope.cboStakeholdertype.usertype_gid;
                    lsuser_type = $scope.cboStakeholdertype.user_type;
                }
                if ($scope.cboCreditAssessmentagency != undefined || $scope.cboCreditAssessmentagency != null) {
                    lsassessmentagency_gid = $scope.cboCreditAssessmentagency.assessmentagency_gid;
                    lsassessmentagency_name = $scope.cboCreditAssessmentagency.assessmentagency_name;
                }
                if ($scope.cboAssessmentRating != undefined || $scope.cboAssessmentRating != null) {
                    lsassessmentagencyrating_gid = $scope.cboAssessmentRating.assessmentagencyrating_gid;
                    lsassessmentagencyrating_name = $scope.cboAssessmentRating.assessmentagencyrating_name;
                }
                if ($scope.cboAMLCategory != undefined || $scope.cboAMLCategory != null) {
                    lsamlcategory_gid = $scope.cboAMLCategory.amlcategory_gid;
                    lsamlcategory_name = $scope.cboAMLCategory.amlcategory_name;
                }
                if ($scope.cboBusinesscategory != undefined || $scope.cboBusinesscategory != null) {
                    lsbusinesscategory_gid = $scope.cboBusinesscategory.businesscategory_gid;
                    lsbusinesscategory_name = $scope.cboBusinesscategory.businesscategory_name;
                }
                if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                    lsdesignation_gid = $scope.cboDesignation.designation_gid;
                    lsdesignation_type = $scope.cboDesignation.designation_type;
                }

                var params = {
                    company_name: $scope.txtcompany_name,
                    date_incorporation: $scope.txtincorporation_date,
                    businessstartdate: $scope.txtbusinessstart_date,
                    year_business: $scope.txtyearin_business,
                    month_business: $scope.txtmonthsin_business,
                    companypan_no: $scope.txtpan_number,
                    cin_no: $scope.txtcin_no,
                    official_telephoneno: $scope.txtofficialtelephone_number,
                    official_mailid: $scope.txtofficial_mailid,
                    companytype_gid: lscompanytype_gid,
                    companytype_name: lscompanytype_name,
                    stakeholdertype_gid: lsusertype_gid,
                    stakeholder_type: lsuser_type,
                    assessmentagency_gid: lsassessmentagency_gid,
                    assessmentagency_name: lsassessmentagency_name,
                    assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                    assessmentagencyrating_name: lsassessmentagencyrating_name,
                    ratingas_on: $scope.txtratingason_date,
                    amlcategory_gid: lsamlcategory_gid,
                    amlcategory_name: lsamlcategory_name,
                    businesscategory_gid: lsbusinesscategory_gid,
                    businesscategory_name: lsbusinesscategory_name,
                    contactperson_firstname: $scope.txtfirst_name,
                    contactperson_middlename: $scope.txtmiddle_name,
                    contactperson_lastname: $scope.txtlast_name,
                    designation_gid: lsdesignation_gid,
                    designation: lsdesignation_type,
                    start_date: $scope.txtstart_date,
                    end_date: $scope.txtend_date,
                    escrow: $scope.rdbescrow,
                    lastyear_turnover: $scope.txtlastyear_turnover,
                    application_gid: $scope.application_gid
                }
                var url = 'api/AgrMstApplicationEdit/SaveInstitutionDtlAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtcompany_name = '';
                        $scope.txtincorporation_date = '';
                        $scope.txtbusinessstart_date = '';
                        $scope.txtyearin_business = '';
                        $scope.txtmonthsin_business = '';
                        $scope.txtpan_number = '';
                        $scope.txtcin_no = '';
                        $scope.txtofficialtelephone_number = '';
                        $scope.txtofficial_mailid = '';
                        $scope.cboCompanytype = '';
                        $scope.cboStakeholdertype = '';
                        $scope.cboCreditAssessmentagency = '';
                        $scope.cboAssessmentRating = '';
                        $scope.txtratingason_date = '';
                        $scope.cboAMLCategory = '';
                        $scope.cboBusinesscategory = '';
                        $scope.txtfirst_name = '';
                        $scope.txtmiddle_name = '';
                        $scope.txtlast_name = '';
                        $scope.txtstart_date = '';
                        $scope.txtend_date = '';
                        $scope.rdbescrow = '';
                        $scope.txtlastyear_turnover = '';
                        $scope.cboDesignation = '';
                        $scope.institutionmobileno_list = '';
                        $scope.institutionmaildetails_list = '';
                        $scope.institutionaddresslist = '';
                        $scope.institutionupload_list = '';
                        $scope.institutionlicense_list = '';
                        window.scrollTo(0, 0);
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.hide_generalsummary = false;
                    $scope.show_generalform = false;
                });
            }
        }
        $scope.submit_institution = function () {
            if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
                Notify.alert('Kindly select Stakeholder Type', 'warning')
            }
            else {
                var lscompanytype_gid = '';
                var lscompanytype_name = '';
                var lsusertype_gid = '';
                var lsuser_type = '';
                var lsassessmentagency_gid = '';
                var lsassessmentagency_name = '';
                var lsassessmentagencyrating_gid = '';
                var lsassessmentagencyrating_name = '';
                var lsbusinesscategory_gid = '';
                var lsbusinesscategory_name = '';
                var lsdesignation_gid = '';
                var lsdesignation_type = '';
                var lsamlcategory_gid = '';
                var lsamlcategory_name = '';

                if ($scope.cboCompanytype != undefined || $scope.cboCompanytype != null) {
                    lscompanytype_gid = $scope.cboCompanytype.companytype_gid;
                    lscompanytype_name = $scope.cboCompanytype.companytype_name;
                }
                if ($scope.cboStakeholdertype != undefined || $scope.cboStakeholdertype != null) {
                    lsusertype_gid = $scope.cboStakeholdertype.usertype_gid;
                    lsuser_type = $scope.cboStakeholdertype.user_type;
                }
                if ($scope.cboCreditAssessmentagency != undefined || $scope.cboCreditAssessmentagency != null) {
                    lsassessmentagency_gid = $scope.cboCreditAssessmentagency.assessmentagency_gid;
                    lsassessmentagency_name = $scope.cboCreditAssessmentagency.assessmentagency_name;
                }
                if ($scope.cboAssessmentRating != undefined || $scope.cboAssessmentRating != null) {
                    lsassessmentagencyrating_gid = $scope.cboAssessmentRating.assessmentagencyrating_gid;
                    lsassessmentagencyrating_name = $scope.cboAssessmentRating.assessmentagencyrating_name;
                }
                if ($scope.cboAMLCategory != undefined || $scope.cboAMLCategory != null) {
                    lsamlcategory_gid = $scope.cboAMLCategory.amlcategory_gid;
                    lsamlcategory_name = $scope.cboAMLCategory.amlcategory_name;
                }
                if ($scope.cboBusinesscategory != undefined || $scope.cboBusinesscategory != null) {
                    lsbusinesscategory_gid = $scope.cboBusinesscategory.businesscategory_gid;
                    lsbusinesscategory_name = $scope.cboBusinesscategory.businesscategory_name;
                }
                if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                    lsdesignation_gid = $scope.cboDesignation.designation_gid;
                    lsdesignation_type = $scope.cboDesignation.designation_type;
                }

                var params = {
                    company_name: $scope.txtcompany_name,
                    date_incorporation: $scope.txtincorporation_date,
                    businessstartdate: $scope.txtbusinessstart_date,
                    year_business: $scope.txtyearin_business,
                    month_business: $scope.txtmonthsin_business,
                    companypan_no: $scope.txtpan_number,
                    cin_no: $scope.txtcin_no,
                    official_telephoneno: $scope.txtofficialtelephone_number,
                    official_mailid: $scope.txtofficial_mailid,
                    companytype_gid: lscompanytype_gid,
                    companytype_name: lscompanytype_name,
                    stakeholdertype_gid: lsusertype_gid,
                    stakeholder_type: lsuser_type,
                    assessmentagency_gid: lsassessmentagency_gid,
                    assessmentagency_name: lsassessmentagency_name,
                    assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                    assessmentagencyrating_name: lsassessmentagencyrating_name,
                    ratingas_on: $scope.txtratingason_date,
                    amlcategory_gid: lsamlcategory_gid,
                    amlcategory_name: lsamlcategory_name,
                    businesscategory_gid: lsbusinesscategory_gid,
                    businesscategory_name: lsbusinesscategory_name,
                    contactperson_firstname: $scope.txtfirst_name,
                    contactperson_middlename: $scope.txtmiddle_name,
                    contactperson_lastname: $scope.txtlast_name,
                    designation_gid: lsdesignation_gid,
                    designation: lsdesignation_type,
                    start_date: $scope.txtstart_date,
                    end_date: $scope.txtend_date,
                    escrow: $scope.rdbescrow,
                    lastyear_turnover: $scope.txtlastyear_turnover,
                    application_gid: $scope.application_gid
                }
                var url = 'api/AgrMstApplicationEdit/SubmitInstitutionDtlAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtcompany_name = '';
                        $scope.txtincorporation_date = '';
                        $scope.txtbusinessstart_date = '';
                        $scope.txtyearin_business = '';
                        $scope.txtmonthsin_business = '';
                        $scope.txtpan_number = '';
                        $scope.txtcin_no = '';
                        $scope.txtofficialtelephone_number = '';
                        $scope.txtofficial_mailid = '';
                        $scope.cboCompanytype = '';
                        $scope.cboStakeholdertype = '';
                        $scope.cboCreditAssessmentagency = '';
                        $scope.cboAssessmentRating = '';
                        $scope.txtratingason_date = '';
                        $scope.cboAMLCategory = '';
                        $scope.cboBusinesscategory = '';
                        $scope.txtfirst_name = '';
                        $scope.txtmiddle_name = '';
                        $scope.txtlast_name = '';
                        $scope.txtstart_date = '';
                        $scope.txtend_date = '';
                        $scope.rdbescrow = '';
                        $scope.txtlastyear_turnover = '';
                        $scope.cboDesignation = '';
                        $scope.institutionmobileno_list = '';
                        $scope.institutionmaildetails_list = '';
                        $scope.institutionaddresslist = '';
                        $scope.institutionupload_list = '';
                        $scope.institutionlicense_list = '';
                        window.scrollTo(0, 0);
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.hide_generalsummary = false;
                    $scope.show_generalform = false;
                });
            }
        }
        $scope.txtlastyear_turnoverchange = function () {
            var input = document.getElementById('lastyear_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtlastyear_turnover = "";
            }
            else {
                $scope.txtlastyear_turnover = output;
                document.getElementById('words_totalamount').innerHTML = lswords_totalamount;
            }
        }

        function license_list() {
            var url = 'api/AgrMstApplicationAdd/GetInstitutionLicenseList';
            SocketService.get(url).then(function (resp) {
                $scope.institutionlicense_list = resp.data.mstlicense_list;

            });
        }

        function institutiongstlist() {
            var url = 'api/AgrMstApplicationAdd/GetInstitutionGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.institutiongst_list = resp.data.mstgst_list;
            });
        }

        function institutionmobilenolist() {
            var url = 'api/AgrMstApplicationAdd/GetInstitutionMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.institutionmobileno_list = resp.data.mstmobileno_list;
            });
        }

        function institutionmail_list() {
            var url = 'api/AgrMstApplicationAdd/GetInstitutionEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.institutionmaildetails_list = resp.data.mstemailaddress_list;
            });
        }

        function institutionaddress_list() {
            var url = 'api/AgrMstApplicationAdd/GetInstitutionAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.institutionaddresslist = resp.data.mstaddress_list;
            });
        }

        $scope.institutiongst_add = function () {
            if (($scope.rdbgstregistered == undefined) || ($scope.rdbgstregistered == '') || ($scope.cboGstState.state_name == undefined) || ($scope.cboGstState.state_name == '') || ($scope.txtgst_number == undefined) || ($scope.txtgst_number == '')) {
                Notify.alert('Enter GST State / Select GST Registered Status / GST Number', 'warning');
            }
            else {
                var params = {
                    gststate_gid: $scope.cboGstState.state_gid,
                    gst_state: $scope.cboGstState.state_name,
                    gst_no: $scope.txtgst_number,
                    gst_registered: $scope.rdbgstregistered
                }
                var url = 'api/AgrMstApplicationAdd/PostInstitutionGST';
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
                    institutiongstlist();
                    $scope.cboGstState = '';
                    $scope.txtgst_number = '';
                    $scope.rdbgstregistered = '';
                });
            }
        }

        $scope.institutiongst_delete = function (institution2branch_gid) {
            var params =
                {
                    institution2branch_gid: institution2branch_gid
                }
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionGST';
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
                institutiongstlist();
            });

        }

        $scope.institutionmobileno_add = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbprimarywhatsapp_no == undefined) || ($scope.rdbprimarymobile_no == '') || ($scope.rdbprimarywhatsapp_no == '')) {
                Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimarymobile_no,
                    whatsapp_no: $scope.rdbprimarywhatsapp_no
                }
                var url = 'api/AgrMstApplicationAdd/PostInstitutionMobileNo';
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
                    institutionmobilenolist();
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbprimarywhatsapp_no = '';
                });
            }
        }

        $scope.institutionmobileno_delete = function (institution2mobileno_gid) {
            var params =
                {
                    institution2mobileno_gid: institution2mobileno_gid
                }
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionMobileNo';
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
                institutionmobilenolist();
            });

        }

        $scope.add_institutiomaildetails = function () {
            if (($scope.txtinstitutionmail_address == undefined) || ($scope.txtinstitutionmail_address == '') || ($scope.rdbinstitutiomaildetails == undefined) || ($scope.rdbinstitutiomaildetails == '')) {
                Notify.alert('Enter Mail ID / Select Primary Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtinstitutionmail_address,
                    primary_status: $scope.rdbinstitutiomaildetails
                }
                var url = 'api/AgrMstApplicationAdd/PostInstitutionEmailAddress';
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
                    institutionmail_list();
                    $scope.txtinstitutionmail_address = '';
                    $scope.rdbinstitutiomaildetails = '';
                });
            }
        }

        $scope.institutionmail_delete = function (institution2email_gid) {
            var params =
                {
                    institution2email_gid: institution2email_gid
                }
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionEmailAddress';
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

                institutionmail_list();
            });

        }


        $scope.addinstitutionaddress = function () {
            var modalInstance = $modal.open({
                templateUrl: '/institutionaddresstype.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AgrMstAddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/AgrMstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                }

                $scope.txtcountry = "India";
                $scope.institutionaddressSubmit = function () {

                    var params = {
                        address_typegid: $scope.cboaddresstype.address_gid,
                        address_type: $scope.cboaddresstype.address_type,
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        landmark: $scope.txtLand_Mark,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry
                    }
                    var url = 'api/AgrMstApplicationAdd/PostInstitutionAddressDetail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            institutionaddress_list();
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

        $scope.deleteinstitution_address = function (institution2address_gid) {
            var params =
                {
                    institution2address_gid: institution2address_gid
                }
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionAddressDetail';
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
                institutionaddress_list();
            });

        }

        // Institution License Add

        $scope.institutionlicenseadd = function () {

            if (($scope.cboLicenseType == '') || ($scope.cboLicenseType == undefined) || ($scope.txtnumber == '') || ($scope.txtnumber == undefined) || ($scope.txtissue_date == '') || ($scope.txtissue_date == undefined) || ($scope.txtexpiry_date == '') || ($scope.txtexpiry_date == undefined)) {
                Notify.alert('Kindly Fill All Mandatory Fields', 'warning');
            }
            else {
                var params = {
                    licensetype_gid: $scope.cboLicenseType.licensetype_gid,
                    licensetype_name: $scope.cboLicenseType.licensetype_name,
                    license_number: $scope.txtnumber,
                    licenseissue_date: $scope.txtissue_date,
                    licenseexpiry_date: $scope.txtexpiry_date

                }
                var url = 'api/AgrMstApplicationAdd/PostInstitutionLicenseDetail';
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
                    license_list();
                    $scope.cboLicenseType = '';
                    $scope.txtnumber = '';
                    $scope.txtissue_date = '';
                    $scope.txtexpiry_date = '';
                });
            }
        }

        $scope.Deleteindividual = function (contact_gid) {
            var params = {
                contact_gid: contact_gid
            }
            var url = 'api/AgrMstApplicationAdd/Deleteindividual';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/AgrMstApplicationAdd/GetIndividualSummary';
                    SocketService.get(url).then(function (resp) {
                        $scope.individual_list = resp.data.cicindividual_list;
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
        }
        $scope.DeleteInstitution = function (institution_gid) {
            var params = {
                institution_gid: institution_gid
            }
            var url = 'api/AgrMstApplicationAdd/Deleteinstitution';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/AgrMstApplicationAdd/GetCICInstitutionSummary';
                    SocketService.get(url).then(function (resp) {
                        $scope.institution_list = resp.data.cicinstitution_list;
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
        }
        $scope.delete_group = function (group_gid) {
            var params = {
                group_gid: group_gid
            }
            var url = 'api/AgrMstApplicationAdd/DeleteGroup';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/AgrMstApplicationAdd/GetGroupSummary';
                    SocketService.get(url).then(function (resp) {
                        unlockUI();
                        $scope.group_list = resp.data.group_list;
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

        // Institution license Delete

        $scope.institutionlicense_delete = function (institution2licensedtl_gid) {
            var params = {
                institution2licensedtl_gid: institution2licensedtl_gid
            }
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionLicenseDetail';
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
                license_list();
            });

        }


        $scope.InstitutionDocumentUpload = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbocompanydocumentname == null) || ($scope.cbocompanydocumentname == '') || ($scope.cbocompanydocumentname == undefined)) {
                $("#institutionfile").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
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
                frm.append('document_title', $scope.cbocompanydocumentname.companydocument_name);
                frm.append('companydocument_gid', $scope.cbocompanydocumentname.companydocument_gid); 
                frm.append('document_id', $scope.txtdocument_id);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/AgrMstApplicationAdd/InstitutionDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.institutionupload_list = resp.data.institutionupload_list;
                        unlockUI();

                        $("#institutionfile").val('');
                        $scope.cbocompanydocumentname = "";
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
        }

        $scope.institutiondocument_delete = function (institution2documentupload_gid) {
            lockUI();
            var params = {
                institution2documentupload_gid: institution2documentupload_gid
            }
            var url = 'api/AgrMstApplicationAdd/InstitutionDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionupload_list = resp.data.institutionupload_list;
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

        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls = true;
            }
        }

        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationInstitutionEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Institution_dtls = true;
            }
        }
        $scope.Group_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationGroupEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Group_dtls = true;
            }
        }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationIndividualEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Individual_dtls = true;
            }
        }

        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationSocialTradeCapitalEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.EconomicCapital_dtls = true;
            }
        }

        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            console.log(applicant_type)
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ProductCharges_dtls = true;
            }
            else {
                $location.url('app/AgrMstApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.EditHypothecation = function (application_gid) {
            $location.url('app/AgrMstApplicationHypothecationEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
        }
        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationEditCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.BureauUpdates_dtls = true;
            }
        }

        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ProductCharges_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditProductAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ServiceCharges_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditChargeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
      
        $scope.Tradeclick = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Trade_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditTradeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }


        $scope.importIndividual = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importIndividual.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.application_gid = application_gid;

                var params = {
                    application_gid: application_gid
                }

                var url = 'api/AgrMstApplicationView/GetIndividualImportLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.individualimport_List = resp.data.individualimport_List;
                });



                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_individual = function () {
                    //var filename = "ImportExcelIndividual.xlsx";
                    ////var phyPath = resp.data.file_path;
                    //var phyPath = "E:\\Web\\EMS\\templates\\ImportExcelIndividual.xlsx";
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = "http://"
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = filename.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelIndividual.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();

                }

                $scope.uploadIndividual = function (val, val1, name) {
                    var application_gid = localStorage.getItem("application_gid");

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelIndividual") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
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
                        frm.append('application_gid', application_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                    }
                }


                $scope.uploadExcelIndividual = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/AgrMstApplicationAdd/ImportExcelIndividual';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            if (resp.data.status == true) {
                                activate();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadExcelCancel = function () {
                    $("#fileimport").val('');
                };



            }
        }

        $scope.importInstitution = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importInstitution.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.application_gid = application_gid;

                var params = {
                    application_gid: application_gid
                }

                var url = 'api/AgrMstApplicationView/GetInstitutionImportLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.institutionimport_List = resp.data.institutionimport_List;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_institution = function () {
                    //var filename = "ImportExcelInstitution.xlsx";
                    ////var phyPath = resp.data.file_path;
                    //var phyPath = "E:\\Web\\EMS\\templates\\ImportExcelInstitution.xlsx";
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = "http://"
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = filename.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelInstitution.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();
                }

                $scope.uploadInstitution = function (val, val1, name) {
                    var application_gid = localStorage.getItem("application_gid");

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelInstitution") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
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
                        frm.append('application_gid', application_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                    }
                }


                $scope.uploadExcelInstitution = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')

                    }
                    else {
                        var url = 'api/AgrMstApplicationAdd/ImportExcelInstitution';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            if (resp.data.status == true) {
                                activate();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadExcelCancel = function () {
                    $("#fileimport").val('');
                };



            }
        }

        $scope.importGroup = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importGroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.application_gid = application_gid;

                var params = {
                    application_gid: application_gid
                }

                var url = 'api/AgrMstApplicationView/GetGroupImportLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.groupimport_List = resp.data.groupimport_List;
                });

                //var url = 'api/BureauAPI/GetHighmarkCreditInfo';
                //SocketService.get(url).then(function (resp) {
                //});

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_group = function () {
                    //var filename = "ImportExcelGroup.xlsx";
                    ////var phyPath = resp.data.file_path;
                    //var phyPath = "E:\\Web\\EMS\\templates\\ImportExcelGroup.xlsx";
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = "http://"
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = filename.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelGroup.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();
                }

                $scope.uploadGroup = function (val, val1, name) {
                    var application_gid = localStorage.getItem("application_gid");

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelGroup") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
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
                        frm.append('application_gid', application_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                    }
                }


                $scope.uploadExcelGroup = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')

                    }
                    else {
                        var url = 'api/AgrMstApplicationAdd/ImportExcelGroup';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            if (resp.data.status == true) {
                                activate();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadExcelCancel = function () {
                    $("#fileimport").val('');
                };



            }
        }

        $scope.hierarchy_change = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/HierarchyChange.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            var application_gid = $scope.application_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    application_gid: application_gid
                }

                var url = 'api/AgrMstApplicationAdd/FnKycDcoumentValidation';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == false) {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $modalInstance.close('closed');
                    }
                    else {

                    }

                });


                var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyChangeList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rm_name = resp.data.rm_name;
                    $scope.directreportingto_name = resp.data.directreportingto_name;
                    $scope.clustermanager_gid = resp.data.clustermanager_gid;
                    $scope.clustermanager_name = resp.data.clustermanager_name;
                    $scope.regionalhead_gid = resp.data.regionalhead_gid;
                    $scope.regionhead_name = resp.data.regionhead_name;
                    $scope.zonalhead_gid = resp.data.zonalhead_gid;
                    $scope.zonalhead_name = resp.data.zonalhead_name;
                    $scope.businesshead_gid = resp.data.businesshead_gid;
                    $scope.businesshead_name = resp.data.businesshead_name;
                });

                $scope.Update_hierarchy = function () {
                    var params = {
                        application_gid: application_gid,
                        clustermanager_gid: $scope.clustermanager_gid,
                        clustermanager_name: $scope.clustermanager_name,
                        regionalhead_gid: $scope.regionalhead_gid,
                        regionalhead_name: $scope.regionhead_name,
                        zonalhead_gid: $scope.zonalhead_gid,
                        zonalhead_name: $scope.zonalhead_name,
                        businesshead_gid: $scope.businesshead_gid,
                        businesshead_name: $scope.businesshead_name
                    }
                    var url = 'api/AgrMstApplicationAdd/UpdateApprovalHierarchyChange';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                    });
                    $modalInstance.close('closed');
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

    }

})();

