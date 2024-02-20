
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('limitInfoaddController', limitInfoaddController);

    limitInfoaddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$parse', 'DownloaddocumentService','cmnfunctionService'];

    function limitInfoaddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $parse, DownloaddocumentService,cmnfunctionService) {

        var vm = this;
        vm.title = 'limitInfoaddController';

        activate();

        function activate() {

            $scope.lsacreate_gid = localStorage.getItem('lsacreate_gid');

            $scope.document_panel = true;
            $scope.panel = true;
            $scope.panel1 = true;
            $scope.facility = false;
            $scope.amount_validation = false;
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
            $scope.onchangefacility = false;
            var date = new Date(),
           mnth = ("0" + (date.getMonth() + 1)).slice(-2),
           day = ("0" + date.getDate()).slice(-2);
            $scope.txtdate = [day, mnth, date.getFullYear()].join("-");
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1

            };
           
            var params=
                {
                    lsacreate_gid: $scope.lsacreate_gid
                }
            var url = 'api/IdasTrnLsaManagement/Getsanction2loanfacility';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loanfacility_list = resp.data.loanfacility_list;
               
            });
            var url = 'api/IdasTrnLsaManagement/limitref_no';
         
            SocketService.getparams(url, params).then(function (resp) {
                $scope.limitinfo_limit = resp.data.limitinfo_limit;
            
            });
            var url = 'api/IdasTrnLsaManagement/ODLIM';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtodlim = resp.data.odlim;
                if (resp.data.total_document_limit == null)
                {
                    $scope.limit = "0,0";
                }
                else {
                    $scope.limit = resp.data.total_document_limit;
                }
               
            });
            var url = 'api/IdasTrnLsaManagement/limitinfodtl';
         
            SocketService.getparams(url, params).then(function (resp) {
                $scope.limitinfo_limit = resp.data.limitinfo_limit;
                $scope.total_document_limit = resp.data.total_document_limit;
                $scope.totol_limit_released = resp.data.totol_limit_released;
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
        $scope.loanfacility_amount = function () {
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
            }
           
            $scope.warningfacility_amount = true;
              }
        $scope.lsalimitinfo_submit = function (lsacreate_gid) {
            if ($scope.text == $scope.txtrate_interest)
            {
                lockUI()
            var params = {
                lsacreate_gid: $scope.lsacreate_gid,
                facility_type: $scope.loanmaster_gid.loan_title,
                facility_type_gid:$scope.loanmaster_gid.loanmaster_gid,
                margin: $scope.txtmargin,
                existing_limit: $scope.txtexistinglimit,
                document_limit: $scope.txtdocument_limit,
                limit_released: $scope.txtlimit_released,
                tenure: $scope.txttenure,
                revolving_type: $scope.rdbrevolving,
                sub_limit: $scope.txtsub_limit,
                rate_interest: $scope.txtrate_interest,
                expirydate: $scope.txtexpiry_date,
                limitinfo_remarks: $scope.txtremarks,
                limit_validation:$scope.limit,
                interchangeability: $scope.rdbinterchangeability,
                report_structure:$scope.cboreport_structure,
                odlim: $scope.txtodlim,
                loanfacility_amount: $scope.txtloanfacility_amount,
                change_request: $scope.rdbchangerequest
            };
           
            var url = 'api/IdasTrnLsaManagement/postlimitinfo';
           
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $state.go('app.lsaManagementadd');
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
                $scope.document_panel = false;
                if ($scope.uploadfrm != undefined) {
                    lockUI()
                    var url = 'api/IdasTrnLsaManagement/PostROIDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        var params = {
                            lsacreate_gid: $scope.lsacreate_gid,
                            facility_type: $scope.loanmaster_gid.loan_title,
                            facility_type_gid: $scope.loanmaster_gid.loanmaster_gid,
                            margin: $scope.txtmargin,
                            existing_limit: $scope.txtexistinglimit,
                            document_limit: $scope.txtdocument_limit,
                            limit_released: $scope.txtlimit_released,
                            tenure: $scope.txttenure,
                            revolving_type: $scope.rdbrevolving,
                            sub_limit: $scope.txtsub_limit,
                            rate_interest: $scope.txtrate_interest,
                            expirydate: $scope.txtexpiry_date,
                            limitinfo_remarks: $scope.txtremarks,
                            limit_validation: $scope.limit,
                            interchangeability: $scope.rdbinterchangeability,
                            report_structure: $scope.cboreport_structure,
                            odlim: $scope.txtodlim,
                            loanfacility_amount: $scope.txtloanfacility_amount,
                            change_request: $scope.rdbchangerequest
                        };

                        var url = 'api/IdasTrnLsaManagement/postlimitinfo';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI()
                                activate();
                                $state.go('app.lsaManagementadd');
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
        $scope.bankdetails = function (lsacreate_gid) {
            var params = {
                lsacreate_gid: $scope.lsacreate_gid,
                bank_name: $scope.txtbank_name,
                account_no: $scope.txtaccount_no,
                ifsc_code: $scope.txtifsc_code,
            };

            var url = 'api/IdasTrnLsaManagement/postbankinfo';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
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
                activate();
            });
            $scope.txtbank_name = '';
            $scope.txtaccount_no = '';
            $scope.txtifsc_code = '';
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
                $scope.txtexistinglimit = "";

            }
            else {
                document.getElementById('existinglimit_words').innerHTML = lsexistinglimit_words;
                $scope.txtexistinglimit = amount;
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
                $scope.txtdocument_limit = "";

            }
            else {
                document.getElementById('documentlimit_words').innerHTML = lsdocumentlimit_words;
           
            $scope.txtdocument_limit = amount;
            if ((($scope.txtodlim.replace(/[\s,]+/g, '').trim())-($scope.limit.replace(/[\s,]+/g, '').trim())) < ($scope.txtdocument_limit.replace(/[\s,]+/g, '').trim())) {
                $scope.panel1 = false;
            }
            else {
                $scope.panel1 = true;
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
                $scope.txtlimit_released = "";

            }
            else {
                document.getElementById('limitreleased_words').innerHTML = lslimitreleased_words;
                $scope.txtlimit_released = amount;


                if (($scope.txtlimit_released.replace(/[\s,]+/g, '').trim()) > ($scope.txtdocument_limit.replace(/[\s,]+/g, '').trim() - $scope.txtexistinglimit.replace(/[\s,]+/g, '').trim())) {
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
        $scope.facilityamount=function()
        {
            var params = {
                lsacreate_gid: $scope.lsacreate_gid,
                loanfacility_gid: $scope.loanmaster_gid.loanmaster_gid,
            };
           
            var url = 'api/IdasTrnLsaManagement/Getloanfacilityamount';
            SocketService.post(url, params).then(function (resp) {
                $scope.txtloanfacility_amount = resp.data.loanfacility_amount;
                $scope.txtdocument_limit = resp.data.document_limit;
                $scope.txtmargin = resp.data.margin;
                $scope.txttenure = resp.data.tenure;
                $scope.rdbinterchangeability = resp.data.interchangeability;
                $scope.txtexpiry_date = new Date(resp.data.expiry_date);            
                $scope.rdbrevolving = resp.data.revolving_type;
                $scope.cboreport_structure = resp.data.report_structure;
                if (resp.data.interchangeability == 'Yes')
                {
                    $scope.interchangeabilityyes = true;
                   
                }
                else
                {
                    $scope.interchangeabilityyes = false;
                }
                $scope.onchangefacility = true;
                $scope.amount_validation = true;
                $scope.loanfacilityref_list = resp.data.loanfacilitytype_list;
                if (resp.data.existing_limit == null) {
                    $scope.txtexistinglimit = "0,0";
                }
                else {
                    $scope.txtexistinglimit = resp.data.existing_limit;
                }
                $scope.txtrate_interest = resp.data.proposed_roi;
                $scope.text = resp.data.proposed_roi;
            });
          
        }
    }
})();
