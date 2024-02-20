
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('limitInfoEditController', limitInfoEditController);

    limitInfoEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$parse', 'DownloaddocumentService','cmnfunctionService'];

    function limitInfoEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $parse, DownloaddocumentService,cmnfunctionService) {

        var vm = this;
        vm.title = 'limitInfoEditController';

        activate();

        function activate() {

            $scope.limitinfodtl_gid = localStorage.getItem('limitinfodtl_gid');

            $scope.document = false;
            $scope.panel = true;
            $scope.panel1 = true;
            $scope.panel2 = true;
            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            // Calender Popup... //

            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            $scope.interchangeabilityno = true;
            $scope.interchangeabilityyes = false;
            
            
            var param = {
                limitinfodtl_gid: $scope.limitinfodtl_gid
            };
            var url = 'api/IdasTrnLsaManagement/editsanction2loanfacility';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.loanfacility_list = resp.data.loanfacility_list;
            });
            var url = 'api/IdasTrnLsaManagement/GetditLimitInfo';
           
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cbonode_type = resp.data.node_edit;
                $scope.txtmargin_edit = resp.data.margin_edit;
              //  $scope.txtexpiry_date_edit = resp.data.expiry_date_edit;
                $scope.txtexpiry_date_edit = new Date(resp.data.expirydate_edit);
                $scope.txtdocument_limit_edit = resp.data.document_limit_edit;
                $scope.txtlimit_released_edit = resp.data.limit_released_edit;
                $scope.txttenure_edit = resp.data.tenure_edit;
                $scope.rdbrevolving_edit = resp.data.revolving_type_edit;
                $scope.txtrate_interest_edit = resp.data.rate_interest_edit;
                $scope.txtsub_limit_edit = resp.data.sub_limit_edit;
                $scope.txtremarks_edit = resp.data.limitinfo_remarks_edit;
                $scope.txtexistinglimit_edit = resp.data.existing_limit_edit;
                $scope.cboloanmaster_gid = resp.data.facility_type_gid;
                if (resp.data.interchangeability == 'Yes')
                {
                    $scope.interchangeabilityno = false;
                    $scope.interchangeabilityyes = true;
                }
                else {
                    $scope.interchangeabilityno = true;
                    $scope.interchangeabilityyes = false;
                }
                $scope.rdbinterchangeability = resp.data.interchangeability;
                $scope.limitref_no = resp.data.limitref_no;
                $scope.txtodlim = resp.data.odlim;
                
                $scope.txtloanfacility_amount = resp.data.facility_amount;
                if ((resp.data.total_document_limit == null) || (resp.data.total_document_limit == '') ){
                    $scope.limit = "0,0";
                }
                else {
                    $scope.limit = resp.data.total_document_limit;
                }
                $scope.loanfacilityref_list = resp.data.loanfacilitytype_list;
                $scope.cboreport_structureedit = resp.data.report_structure;
                $scope.rdbchangerequest = resp.data.change_request;
                $scope.text = resp.data.rate_interest_edit;
                $scope.document_name = resp.data.document_name;
                $scope.document_path = resp.data.document_path;
                console.log(resp.data.total_document_limit)
                unlockUI();
            });
        
            
        }

        // Numeric to Word - Indian Standard...//

        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }
        $scope.lsaback = function () {
            $state.go('app.lsaManagementadd');
        }
        $scope.facilitytype_amount = function () {
            var input = document.getElementById('txtInputloanfacility_type').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-IN');
            var lsloanfacilityamount_words = inWords(str);
            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtloanfacility_amount = "";

            }
            else {
                document.getElementById('loanfacilityamount_words').innerHTML = lsloanfacilityamount_words;
            $scope.txtloanfacility_amount = output;
            $scope.warningfacility_amount = true;
            }

        }
        $scope.limitinfo_update = function (limitinfodtl_gid) {
            if ($scope.text == $scope.txtrate_interest_edit)
            {

            var loantitle = $('#loan_title :selected').text();
            var params = {
                limitinfodtl_gid: $scope.limitinfodtl_gid,
                facility_type_edit: loantitle,
                facility_type_gid: $scope.cboloanmaster_gid,
                margin_edit: $scope.txtmargin_edit,
                existing_limit_edit: $scope.txtexistinglimit_edit,
                document_limit_edit: $scope.txtdocument_limit_edit,
                limit_released_edit: $scope.txtlimit_released_edit,
                tenure_edit: $scope.txttenure_edit,
                revolving_type_edit: $scope.rdbrevolving_edit,
                sub_limit_edit: $scope.txtsub_limit_edit,
                rate_interest_edit: $scope.txtrate_interest_edit,
               // expirydate_edit: $scope.txtexpiry_date_edit,
                limitinfo_remarks_edit: $scope.txtremarks_edit,
                node_edit: $scope.cbonode_type,
                interchangeability: $scope.rdbinterchangeability,
                expiry_date_edit: $scope.txtexpiry_date_edit,
                odlim: $scope.txtodlim,
                report_structure: $scope.cboreport_structureedit,
                limit_validation: $scope.limit,
                facility_amount: $scope.txtloanfacility_amount,
                change_request: $scope.rdbchangerequest
            };
            
            var url = 'api/IdasTrnLsaManagement/updatelimitinfo';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    $state.go('app.lsaManagementadd');
                    activate();
                 
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

            }
            else {
                if (($scope.document_name == '') || ($scope.document_name == null))
                   { 
                $scope.document = true;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/IdasTrnLsaManagement/PostROIDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        var loantitle = $('#loan_title :selected').text();
                        var params = {
                            limitinfodtl_gid: $scope.limitinfodtl_gid,
                            facility_type_edit: loantitle,
                            facility_type_gid: $scope.cboloanmaster_gid,
                            margin_edit: $scope.txtmargin_edit,
                            existing_limit_edit: $scope.txtexistinglimit_edit,
                            document_limit_edit: $scope.txtdocument_limit_edit,
                            limit_released_edit: $scope.txtlimit_released_edit,
                            tenure_edit: $scope.txttenure_edit,
                            revolving_type_edit: $scope.rdbrevolving_edit,
                            sub_limit_edit: $scope.txtsub_limit_edit,
                            rate_interest_edit: $scope.txtrate_interest_edit,
                            limitinfo_remarks_edit: $scope.txtremarks_edit,
                            node_edit: $scope.cbonode_type,
                            interchangeability: $scope.rdbinterchangeability,
                            expiry_date_edit: $scope.txtexpiry_date_edit,
                            odlim: $scope.txtodlim,
                            report_structure: $scope.cboreport_structureedit,
                            limit_validation: $scope.limit,
                            facility_amount: $scope.txtloanfacility_amount,
                            change_request: $scope.rdbchangerequest
                        };
                        console.log(params);
                        var url = 'api/IdasTrnLsaManagement/updatelimitinfo';
                        lockUI()
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI()
                                $state.go('app.lsaManagementadd');
                                activate();
                 
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }

                        });
                    });
                }
                   
                else {
                    Notify.alert('ROI amount is changed. Kindly upload the reference document');
                }
                }
            else{

                    var loantitle = $('#loan_title :selected').text();
                    var params = {
                        limitinfodtl_gid: $scope.limitinfodtl_gid,
                        facility_type_edit: loantitle,
                        facility_type_gid: $scope.cboloanmaster_gid,
                        margin_edit: $scope.txtmargin_edit,
                        existing_limit_edit: $scope.txtexistinglimit_edit,
                        document_limit_edit: $scope.txtdocument_limit_edit,
                        limit_released_edit: $scope.txtlimit_released_edit,
                        tenure_edit: $scope.txttenure_edit,
                        revolving_type_edit: $scope.rdbrevolving_edit,
                        sub_limit_edit: $scope.txtsub_limit_edit,
                        rate_interest_edit: $scope.txtrate_interest_edit,
                        // expirydate_edit: $scope.txtexpiry_date_edit,
                        limitinfo_remarks_edit: $scope.txtremarks_edit,
                        node_edit: $scope.cbonode_type,
                        interchangeability: $scope.rdbinterchangeability,
                        expiry_date_edit: $scope.txtexpiry_date_edit,
                        odlim: $scope.txtodlim,
                        report_structure: $scope.cboreport_structureedit,
                        limit_validation: $scope.limit,
                        facility_amount: $scope.txtloanfacility_amount,
                        change_request: $scope.rdbchangerequest
                    };

                    var url = 'api/IdasTrnLsaManagement/updatelimitinfo';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI()
                            $state.go('app.lsaManagementadd');
                            activate();

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });
                }
            }
        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //console.log(val1);
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
        }
        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lsexistinglimit_words = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (amount == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtexistinglimit_edit = "";

            }
            else {
                document.getElementById('existinglimit_words').innerHTML = lsexistinglimit_words;
                $scope.txtexistinglimit_edit = amount;
              
            }
           
        }
        $scope.amountschange1 = function () {
            var input = document.getElementById('txtInput1').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lsdocumentlimit_words = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (amount == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdocument_limit_edit = "";

            }
            else {

                document.getElementById('documentlimit_words').innerHTML = lsdocumentlimit_words;
            $scope.txtdocument_limit_edit = amount;
           if ((($scope.txtodlim.replace(/[\s,]+/g, '').trim()) - ($scope.limit.replace(/[\s,]+/g, '').trim())) < ($scope.txtdocument_limit_edit.replace(/[\s,]+/g, '').trim())) {
                $scope.panel2 = false;
            }
            else {
                $scope.panel2 = true;
           }

            }
        }
        $scope.amountschange2 = function () {
            var input = document.getElementById('txtInput2').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lslimitreleased_words = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (amount == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtlimit_released_edit = "";

            }
            else {
                document.getElementById('limitreleased_words').innerHTML = lslimitreleased_words;
                $scope.txtlimit_released_edit = amount;


                if (($scope.txtlimit_released_edit.replace(/[\s,]+/g, '').trim()) > ($scope.txtdocument_limit_edit.replace(/[\s,]+/g, '').trim() - $scope.txtexistinglimit_edit.replace(/[\s,]+/g, '').trim())) {
                    $scope.panel = false;
                }
                else {
                    $scope.panel = true;
                }
            }
        }
        $scope.amountschange3 = function () {
            var input = document.getElementById('txtInput3').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-IN');
            var lsodlim_words = inWords(str);
            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtodlim = "";

            }
            else {

                document.getElementById('odlim_words').innerHTML = lsodlim_words;
                $scope.txtodlim = output;
            }

        }
        $scope.interchangeability_yes = function () {
            $scope.interchangeabilityno = false;
            $scope.interchangeabilityyes = true;
        }
        $scope.interchangeability_no = function () {
            $scope.interchangeabilityno = true;
            $scope.interchangeabilityyes = false;
        }
        $scope.facilityamount = function () {
            var params = {
                limitinfodtl_gid: $scope.limitinfodtl_gid,
                loanfacility_gid: $scope.cboloanmaster_gid,
            };
            var url = 'api/IdasTrnLsaManagement/GetEditloanfacilityamount';
            SocketService.post(url, params).then(function (resp) {
                $scope.txtloanfacility_amount = resp.data.loanfacility_amount;
                $scope.txtdocument_limit_edit = resp.data.document_limit;
                $scope.txtmargin_edit = resp.data.margin;
                $scope.txttenure_edit = resp.data.tenure;
                $scope.rdbinterchangeability = resp.data.interchangeability;
                //$scope.txtexpiry_date_edit = resp.data.expiry_date;
                $scope.txtexpiry_date_edit = new Date(resp.data.expiry_date);
                $scope.rdbrevolving_edit = resp.data.revolving_type;
                $scope.cboreport_structureedit = resp.data.report_structure;
                if (resp.data.interchangeability == 'Yes') {
                    $scope.interchangeabilityyes = true;

                }
                else {
                    $scope.interchangeabilityyes = false;
                }
               
                $scope.onchangefacility = true;
                $scope.amount_validation = true;
                $scope.loanfacilityref_list = resp.data.loanfacilitytype_list;
                $scope.text = resp.data.proposed_roi;
            });
        }

        $scope.cancel = function (limitinfodtl_gid)
        {
            var params = {
                limitinfodtl_gid: limitinfodtl_gid
            }
            var url = 'api/IdasTrnLsaManagement/CancelDocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $scope.document = true;
            });
        }
    }
})();
