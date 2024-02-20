(function () {
    'use strict';

    angular
        .module('angle')
        .controller('invoiceCreatecontroller', invoiceCreatecontroller);

    invoiceCreatecontroller.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function invoiceCreatecontroller($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'invoiceCreatecontroller';

        activate();

        function activate() {
            $scope.servicetype_others = false;
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = "api/LawyerInvoice/GetCaseType";
            SocketService.get(url).then(function (resp) {
                $scope.casestype_list = resp.data.casestype_list;
              
            });
            var url = "api/LawyerInvoice/tmpdocumentdelete";
            SocketService.get(url).then(function (resp) {
            });
            var url = "api/lglMstServiceType/getservicetype2invoice";
            SocketService.get(url).then(function (resp) {
                $scope.servicetype_data = resp.data.servicetype_list;
              
            });
        }

        $scope.invoiceamountchange = function () {

            var input = document.getElementById('txtInput').value;
            var str = input.replace(/,/g, '');

            var output = Number(str).toLocaleString('en-IN');
            var lswordsinvoiceamount= inWords(str);


            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtinvoice_amount = "";

            }
            else {
                document.getElementById('wordsinvoiceamount').innerHTML = lswordsinvoiceamount;

                $scope.txtinvoice_amount = output;
                
            }
        }
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
        $scope.onselectedservicetype=function()
        {
          
        if ($scope.cboservice_type.service_type == "Others")
        {
            $scope.servicetype_others = true;
        }
        else
        {
            $scope.servicetype_others = false;
        }

        }
        $scope.cancel = function () {
            $state.go('app.invoiceSummary');
        }

        $scope.createInvoiceSubmit = function () {
            if ($scope.cboservice_type.service_type == 'Others')
            {

            if (($scope.txtserviceypeothers_title == '') || ($scope.txtserviceypeothers_title == undefined))
            {
              
                Notify.alert('Enter Others Title', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {

            
            lockUI();
            var params = {
                invoice_refno: $scope.txtinvoice_refno,
                invoice_date: $scope.txtinvoice_date,
                invoice_amount: $scope.txtinvoice_amount,
                invoice_remarks: $scope.txtinvoice_remarks,
                case_type: $scope.cbocase_type.case_type,
                caseref_no: $scope.cbocaseref_no.caseref_no,
                servicerender_date: $scope.txtservicerender_date,
                service_type: $scope.cboservice_type.service_type,
                servicetype_gid: $scope.cboservice_type.servicetype_gid,
                caseref_gid: $scope.cbocaseref_no.caseref_gid,
              serviceypeothers_title: $scope.txtserviceypeothers_title
            }
            console.log(params);
            var url = "api/LawyerInvoice/postinvoicedetails";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    console.log(resp.data.status);
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                  
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
                $state.go('app.invoiceSummary');
            });
            }
            }
            else {


                lockUI();
                var params = {
                    invoice_refno: $scope.txtinvoice_refno,
                    invoice_date: $scope.txtinvoice_date,
                    invoice_amount: $scope.txtinvoice_amount,
                    invoice_remarks: $scope.txtinvoice_remarks,
                    case_type: $scope.cbocase_type.case_type,
                    caseref_no: $scope.cbocaseref_no.caseref_no,
                    servicerender_date: $scope.txtservicerender_date,
                    service_type: $scope.cboservice_type.service_type,
                    servicetype_gid: $scope.cboservice_type.servicetype_gid,
                    caseref_gid: $scope.cbocaseref_no.caseref_gid,
                    serviceypeothers_title: $scope.txtserviceypeothers_title
                }
                console.log(params);
                var url = "api/LawyerInvoice/postinvoicedetails";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        console.log(resp.data.status);
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        unlockUI();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'Warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                    $state.go('app.invoiceSummary');
                });
            }
        }
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            $scope.uploadfrm = frm;
            var url = 'api/LawyerInvoice/UploadDocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;
                $("#addupload").val('');
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                   
                }
                else {
                    Notify.alert('File Format Not Supported!', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });

        }

        $scope.delete = function (invoice_documentgid) {
            var params = {
                invoice_documentgid: invoice_documentgid
            }
            var url = "api/LawyerInvoice/getcanceldocument";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }
    
        $scope.onselectedchangecasetype=function()
        {
           
            var params = {
                case_type: $scope.cbocase_type.case_type
            }
            console.log(params);
                var url = 'api/LawyerInvoice/Getlegalservices';

                SocketService.getparams(url,params).then(function (resp) {
                    $scope.cases_list = resp.data.cases_list;
                   
                });

         
        }

    }
})();
