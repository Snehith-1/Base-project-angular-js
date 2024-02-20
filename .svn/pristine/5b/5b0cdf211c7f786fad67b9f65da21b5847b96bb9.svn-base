(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lsaManagementaddController', lsaManagementaddController);

    lsaManagementaddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$parse', 'DownloaddocumentService','cmnfunctionService'];

    function lsaManagementaddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $parse, DownloaddocumentService,cmnfunctionService) {

        var vm = this;
        vm.title = 'lsaManagementaddController';

        activate();

        function activate() {

            $scope.lsacreate_gid = localStorage.getItem('lsacreate_gid');
         
            $scope.stepone = false;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = true;

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
            $scope.cersai_no=false;
            $scope.panel1 = false;
            $scope.panel2 = false;
            $scope.panel3 = false;
            $scope.hidephotodiv = true;
            $scope.showphotodiv = false;
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
           
            var params = {
                lsacreate_gid: $scope.lsacreate_gid,

            };
            var url = 'api/IdasTrnLsaManagement/Getlsainfo';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.document_charge_flag = resp.data.document_charge_flag;
                $scope.recover_flag = resp.data.recover_flag;
                $scope.customername = resp.data.customer_name;
                $scope.clarify_flag = resp.data.clarify_flag;
                $scope.compliance_flag = resp.data.compliance_flag;
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.approval_status = resp.data.approval_status;
               
            });
          
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_dropdown = resp.data.employee_list;            
            });
            var url = 'api/IdasTrnLsaManagement/tempdelete';
            SocketService.get(url).then(function (resp) {
               
            });
            var url = 'api/IdasTrnLsaManagement/GetPenalInterest';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.penalinterest_list = resp.data.loanfacilitytype_list;
                console.log(resp.data.loanfacilitytype_list)
            });
            var url = 'api/IdasTrnLsaManagement/limitinfodtl';
         
            SocketService.getparams(url, params).then(function (resp) {
                $scope.limitinfo_limit = resp.data.limitinfo_limit;
                $scope.total_document_limit = resp.data.total_document_limit;
                $scope.totol_limit_released = resp.data.totol_limit_released;
                $scope.final_flag = resp.data.final_flag;
               
            });
            var url = 'api/IdasTrnLsaManagement/bankinfodtl';
          
            SocketService.getparams(url, params).then(function (resp) {
                $scope.bankinfo_list = resp.data.bankinfo_list;

            });
           
            var url = 'api/IdasTrnLsaManagement/loanfacility';
            SocketService.get(url).then(function (resp) {
                $scope.loanfacility_list = resp.data.loanfacility_list;
            });
            var url = 'api/IdasTrnLsaManagement/Getprocessingfeeinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.recovered_type = resp.data.recovered_type;
                $scope.recovered_amount = resp.data.recovered_amount;
                $scope.chequeno_details = resp.data.chequeno_details;
                $scope.chequedate_details = resp.data.chequedate_details;
                $scope.processingfeebank_name = resp.data.processingfeebank_name;
                $scope.processingfeaccount_name = resp.data.processingfeaccount_name;
                $scope.recover_remarks = resp.data.recover_remarks;
                $scope.to_be_recoveredamount = resp.data.to_be_recoveredamount;
              
            });

            var url = 'api/IdasTrnLsaManagement/Getdocumentchargeinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.doc_recovered_amount = resp.data.doc_recovered_amount;
                $scope.doc_chequeno_details = resp.data.doc_chequeno_details;
                $scope.doc_chequedate_details = resp.data.doc_chequedate_details;
                $scope.doc_feebank_name = resp.data.doc_feebank_name;
                $scope.doc_feaccount_name = resp.data.doc_feaccount_name;
                $scope.document_name = resp.data.document_name;
                $scope.document_path = resp.data.document_path;
                $scope.document_charge = new Intl.NumberFormat('en-IN').format(Number(resp.data.document_charge)) + ".00";
                $scope.document_charge_gst=resp.data.document_charge_gst+"%";
                if (resp.data.document_name == null) {
                    $scope.document = false;
                }
                else
                {
                    $scope.document = true;
                }
                $scope.lbldocumentcharge_applicable = resp.data.documentcharge_applicable;
                $scope.lbldocumentcharge_remarks = resp.data.documentcharge_remarks;
            });
            var url = 'api/IdasTrnLsaManagement/Getmakerinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.maker_signature = resp.data.maker_signature;
                console.log(resp.data.maker_signature);
                $scope.terms_conditions = resp.data.terms_conditions;
                $scope.deferral_captured = resp.data.deferral_captured;
                $scope.head = resp.data.head;
              
            });
            var url = 'api/IdasTrnLsaManagement/Getcompliancecheckinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.nach_mandate = resp.data.nach_mandate;
                $scope.sign_match = resp.data.sign_match;
                $scope.sign_match_kyc = resp.data.sign_match_kyc;
                $scope.escrow_opened = resp.data.escrow_opened;
                $scope.appropriate_stamp = resp.data.nach_mandate;
                $scope.roc_filling = resp.data.roc_filling;
                $scope.nach_mandate_remarks = resp.data.nach_mandate_remarks;
                $scope.sign_match_remarks = resp.data.sign_match_remarks;
                $scope.sign_match_kyc_remarks = resp.data.sign_match_kyc_remarks;
                $scope.escrow_opened_remarks = resp.data.escrow_opened_remarks;
                $scope.appropriate_stamp_remarks = resp.data.appropriate_stamp_remarks;
                $scope.roc_filling_remarks = resp.data.roc_filling_remarks;
                $scope.cersai = resp.data.cersai;
                $scope.cersai_remarks = resp.data.cersai_remarks;

            });
            var url = 'api/IdasTrnLsaManagement/Getdocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.filename_list = resp.data.UploadDocumentList;


            });
            var url = 'api/IdasTrnLsaManagement/Getsanction2Colletarl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer2security_list = resp.data.customersecurity_list;
               
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
        $scope.addlimitinfo = function (lsacreate_gid)
        {
            $state.go('app.idasTrnaddLimitinfo');
        }
        $scope.recoveredstatus=function()
        {
            $scope.tobe_recovered = false;
            $scope.already_recovered = true;
                
        }
        $scope.recoveredstatus1 = function () {
            $scope.tobe_recovered = true;
            $scope.already_recovered = false;

        }
        $scope.lsaback = function () {
            $state.go('app.lsaManagement');
        }
      
   
        $scope.addbankinfo = function (lsacreate_gid) {
            var lsacreate_gid = lsacreate_gid;
           
        
            var modalInstance = $modal.open({
                templateUrl: '/customerbankinformation.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.bankdetails = function () {
                   
                    var params = {
                       
                        bank_name: $scope.txtbank_name,
                        account_no: $scope.txtaccount_no,
                        ifsc_code: $scope.txtifsc_code,
                        lsacreate_gid: lsacreate_gid,
                    };
                    var url = 'api/IdasTrnLsaManagement/postbankinfo';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI()
                            
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
                        banklist();
                    });
             
                }
            }
        }
        
        $scope.editbankinfo = function (lsacustomer2bankinfo) {
       
            var lsacustomer2bankinfo = lsacustomer2bankinfo;


            var modalInstance = $modal.open({
                templateUrl: '/customerbankinformation_edit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    lsacustomer2bankinfo: lsacustomer2bankinfo
                }
             
                var url = 'api/IdasTrnLsaManagement/GetBankinfo';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtbank_nameedit = resp.data.bank_name;
                    $scope.txtaccount_noedit = resp.data.account_no;
                    $scope.txtifsc_codeedit = resp.data.ifsc_code;
                    $scope.lsacustomer2bankinfo = resp.data.lsacustomer2bankinfo;
                });
                $scope.bankdetails_edit = function () {

                    var params = {

                        bank_name: $scope.txtbank_nameedit,
                        account_no: $scope.txtaccount_noedit,
                        ifsc_code: $scope.txtifsc_codeedit,
                        lsacustomer2bankinfo:lsacustomer2bankinfo,
                    };
                    var url = 'api/IdasTrnLsaManagement/updatebankinfo';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI()
                            
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
                        banklist();
                    });

                }
            }
        }
        function banklist() {
            var params =
                {
                    lsacreate_gid: $scope.lsacreate_gid,
                }
           
            var url = 'api/IdasTrnLsaManagement/bankinfodtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.bankinfo_list = resp.data.bankinfo_list;

            });
            var url = 'api/IdasTrnLsaManagement/limitinfodtl';

            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.final_flag = resp.data.final_flag;

            });
        }
        $scope.nachform_no = function () {
        
            $scope.nach_no = true;
           
        }
        $scope.signmatch_no = function () {
           
            $scope.signmatching = true;
          
        }
        $scope.signmatch_kyc_no = function () {

            $scope.signmatch_kycprovide = true;

        }
        $scope.escrowac_no = function () {

            $scope.escrow_no = true;

        }
        $scope.stamping_no = function () {

            $scope.stamp_no = true;

        }
        $scope.roc_filling_no = function () {

            $scope.roc_no = true;

        }
        $scope.nachform_yes = function () {

            $scope.nach_no = false;

        }
        $scope.signmatch_yes = function () {

            $scope.signmatching = false;

        }
        $scope.signmatch_kyc_yes = function () {

            $scope.signmatch_kycprovide = false;

        }
        $scope.escrowac_yes = function () {

            $scope.escrow_no = false;

        }
        $scope.stamping_yes = function () {

            $scope.stamp_no = false;

        }
        $scope.roc_filling_yes = function () {

            $scope.roc_no = false;

        }
        $scope.rdbcersai_no = function () {

            $scope.cersai_no = true;

        }
        $scope.rdbcersai_yes = function () {

            $scope.cersai_no = false;

        }
        $scope.clickstep1 = function () {
            $scope.stepone = false;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = true;
        }
        $scope.clickstep2 = function () {
            $scope.stepone = true;
            $scope.steptwo = false;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = true;
        }
        $scope.clickstep3 = function () {
            $scope.stepone = true;
            $scope.steptwo = true;
            $scope.stepthree = false;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = true;
        }
        $scope.clickstep4 = function () {
            $scope.stepone = true;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = false;
            $scope.stepfive = true;
            $scope.stepsix = true;
        }
        $scope.clickstep5 = function () {
            $scope.stepone = true;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = false;
            $scope.stepsix = true;
        }
        $scope.clickstep6 = function () {
            $scope.stepone = true;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = false;
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
                $scope.txtdocument_limit = amount;
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
                $scope.txtlimit_released = amount;

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
            var output = Number(str).toLocaleString('en-US');
            var lsrecoveredamount_words = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (amount == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtrecovered_amount = "";

            }
            else {
                document.getElementById('recoveredamount_words').innerHTML = lsrecoveredamount_words;
                $scope.txtrecovered_amount = amount;

            }

        }
        $scope.amountschange4 = function () {
            var input = document.getElementById('txtInput4').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lsdocumentchargerecoverd_words = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (amount == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdoc_recovered_amount = "";

            }
            else {
                document.getElementById('documentchargerecoverd_words').innerHTML = lsdocumentchargerecoverd_words;
                $scope.txtdoc_recovered_amount = amount;

            }

        }
        $scope.amountschange5 = function () {
            var input = document.getElementById('txtInput5').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lstoberecoveredamount_words = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (amount == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtto_be_recoveredamount = "";

            }
            else {
                document.getElementById('toberecoveredamount_words').innerHTML = lstoberecoveredamount_words;
                $scope.txtto_be_recoveredamount = amount;
            }
        }
        $scope.amountschange04 = function () {
            var input = document.getElementById('txtInput04').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lsdocumentcharge_words = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (amount == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdocument_charges = "";

            }
            else {
                document.getElementById('documentcharge_words').innerHTML = lsdocumentcharge_words;
                $scope.txtdocument_charges = amount;
                if ($scope.txtgst != null) {
                    var amount1 = (($scope.txtdocument_charges.replace(/[\s,]+/g, '').trim()) * ($scope.txtgst) / 100);
                    var doc = ($scope.txtdocument_charges.replace(/[\s,]+/g, '').trim());
                    var output1 = (parseInt(amount1) + parseInt(doc));

                    $scope.txtdoc_recovered_amount = new Intl.NumberFormat('en-IN').format(Number(output1));
                }
            }
        }
        $scope.amountschange10 = function () {
            var amount=(($scope.txtdocument_charges.replace(/[\s,]+/g, '').trim()) * ($scope.txtgst)/100);
            var doc=($scope.txtdocument_charges.replace(/[\s,]+/g, '').trim());
            var output = (parseInt(amount) + parseInt(doc));
            var lsdocumentchargerecoverd_words = inWords(output);
            document.getElementById('documentchargerecoverd_words').innerHTML = lsdocumentchargerecoverd_words;
            $scope.txtdoc_recovered_amount = new Intl.NumberFormat('en-IN').format(Number(output));

        }
        $scope.update_limit = function (limitinfodtl_gid)
        {
            var params = {
                principal: $scope.txtprincipal,
                interest: $scope.txtinterest,
                moratorium: $scope.txtmoratorium,
                calloption: $scope.txtcalloption,
                limitinfodtl_gid: limitinfodtl_gid
            };
         
            var url = 'api/IdasTrnLsaManagement/repaymentdtl';
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
              
            });
        }

        $scope.recoveredsubmit = function (lsacreate_gid)
        {
            var params = {
                recovered_type: $scope.rdbrecovered_type,
                recovered_amount: $scope.txtrecovered_amount,
                chequeno_details: $scope.txtcheque_no,
                chequedate_details: $scope.txtcheque_date,
                processingfeebank_name: $scope.txtrecoveredbank_name,
                processingfeaccount_name: $scope.txtrecoveredaccount_no,
                recover_remarks: $scope.txtrecovered_remarks,
                lsacreate_gid: $scope.lsacreate_gid,
                to_be_recoveredamount: $scope.txtto_be_recoveredamount,

            };
 
            var url = 'api/IdasTrnLsaManagement/processingfee';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    processingfeeslist();
                    
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
                processingfeeslist();
            });
        }
       
        $scope.doc_charges_submit = function (lsacreate_gid) {

            var params = {
                
                doc_feaccount_name: $scope.txtdoc_account_no,
                lsacreate_gid: $scope.lsacreate_gid,
            };
            var url = 'api/IdasTrnLsaManagement/Getaccountno_validation';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if ($scope.txtdocumentcharge_remarks == undefined) {
                    var remarks = 'NA';
                }
                else {
                    var remarks = $scope.txtdocumentcharge_remarks;
                }
                if (resp.data.status == true) {
                    var params = {
                        document_charge:$scope.txtdocument_charges,
                        doc_recovered_amount: $scope.txtdoc_recovered_amount,
                        doc_chequeno_details: $scope.txtdoc_cheque_no,
                        doc_chequedate_details: $scope.txtdoc_cheque_date,
                        doc_feebank_name: $scope.txtdoc_bank_name,
                        doc_feaccount_name: $scope.txtdoc_account_no,
                        lsacreate_gid: $scope.lsacreate_gid,
                        document_charge_gst: $scope.txtgst,
                        documentcharge_applicable: $scope.rdbdocumentcharge,
                        documentcharge_remarks: remarks
                    };
   

                    var url = 'api/IdasTrnLsaManagement/documentcharge';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI()
                    
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
                        documentcharges();
                    });
                }
                else {
                    unlockUI();

                
                    var modalInstance = $modal.open({
                        templateUrl: '/warning.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });

                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                            $scope.panel1 = false;
                            $scope.panel2 = false;
                            $scope.panel3 = false;
                        };
                        $scope.yes = function () {
                          
                            $modalInstance.close('closed');
                          
                            popuplist();
                        };

                      
                    }
                 
                }
              
            });
          
        }
        function  popuplist()
        {
        
            $scope.panel1 = true;
            $scope.panel2 = true;
            $scope.panel3 = true;

        }
        $scope.doc_charge = function (val, val1, name) {
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
            localStorage.setItem($scope.uploadfrm, '$scope.uploadfrm');

            var url = 'api/IdasTrnLsaManagement/Uploaddoc';
            lockUI()
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                unlockUI()
                $("#adduploadphoto").val('');
               
                if (resp.data.status == true) {

                    $scope.hidephotodiv = false;
                    $scope.showphotodiv = true;
                    Notify.alert('Document Uploaded Successfully', 'success');
                }
                else {

                    Notify.alert('Error Occured while uploading', 'warning');
                }


            });

        }
    
        $scope.doc_charges_with_doc = function (lsacreate_gid) {

            var url = 'api/IdasTrnLsaManagement/documentmandatory_check';

            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    if ($scope.txtdocumentcharge_remarks == undefined) {
                        var remarks = 'NA';
                    }
                    else {
                        var remarks = $scope.txtdocumentcharge_remarks;
                    }
                    var url = 'api/IdasTrnLsaManagement/documentchargewithdoc';
                    lockUI()
                    var params = {
                        document_charge: $scope.txtdocument_charges,
                        doc_recovered_amount: $scope.txtdoc_recovered_amount,
                        doc_chequeno_details: $scope.txtdoc_cheque_no,
                        doc_chequedate_details: $scope.txtdoc_cheque_date,
                        doc_feebank_name: $scope.txtdoc_bank_name,
                        doc_feaccount_name: $scope.txtdoc_account_no,
                        lsacreate_gid: $scope.lsacreate_gid,
                        document_charge_gst: $scope.txtgst,
                        documentcharge_applicable: $scope.rdbdocumentcharge,
                        documentcharge_remarks: remarks
                    };
                    console.log(params);
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI()
                         
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
                        documentcharges();
                    });
               
                }
                else {

                    Notify.alert(resp.data.message, 'warning')
                }
            });
           
        }
     
        $scope.edit = function (limitinfodtl_gid) {
           
            var limitinfodtl_gid = limitinfodtl_gid;
            var modalInstance = $modal.open({
                templateUrl: '/editrepayment.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    limitinfodtl_gid: limitinfodtl_gid
                }
                var url = 'api/IdasTrnLsaManagement/Getrepaymentinfo';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtprincipal = resp.data.principal;
                    $scope.txtinterest = resp.data.interest;
                    $scope.txtcalloption = resp.data.call_option;
                    $scope.txtmoratorium = resp.data.moratorium;

                });
          
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                      

                $scope.update_repayment = function () {

                    var params = {
                        principal: $scope.txtprincipal,
                        interest: $scope.txtinterest,
                        call_option: $scope.txtcalloption,
                        moratorium: $scope.txtmoratorium,
                        limitinfodtl_gid: limitinfodtl_gid
                    }


                    var url = 'api/IdasTrnLsaManagement/repaymentdtl';
                    SocketService.post(url, params).then(function (resp) {


                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            repaymentlist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                           
                        }
                        
                    });
                    repaymentlist();

                }
            }
        }

        function repaymentlist()
        {
            var params =
            {
                lsacreate_gid: $scope.lsacreate_gid,
            }
            console.log(params);
            var url = 'api/IdasTrnLsaManagement/limitinfodtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.limitinfo_limit = resp.data.limitinfo_limit;
                $scope.total_document_limit = resp.data.total_document_limit;
                $scope.totol_limit_released = resp.data.totol_limit_released;
                $scope.final_flag = resp.data.final_flag;

            });
            var url = 'api/IdasTrnLsaManagement/limitinfodtl';

            SocketService.getparams(url, params).then(function (resp) {
                
                $scope.final_flag = resp.data.final_flag;

            });
        }
        $scope.final = function (lsacreate_gid)
        {
            var params = {
                terms_conditions: $scope.rdbterms_conditions,
                deferral_captured: $scope.rdbdeferral_captured,            
                head: $scope.credit_manager,
               
                lsacreate_gid: $scope.lsacreate_gid,
            };
     
            var url = 'api/IdasTrnLsaManagement/postfinal';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    finallist();
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
        function finallist() {
            var params =
            {
                lsacreate_gid: $scope.lsacreate_gid,
            }
            var url = 'api/IdasTrnLsaManagement/Getmakerinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.maker_signature = resp.data.maker_signature;
                $scope.terms_conditions = resp.data.terms_conditions;
                $scope.deferral_captured = resp.data.deferral_captured;
                $scope.head = resp.data.head;

            });
            var url = 'api/IdasTrnLsaManagement/Getlsainfo';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.document_charge_flag = resp.data.document_charge_flag;
                $scope.recover_flag = resp.data.recover_flag;
                $scope.customername = resp.data.customer_name;
                $scope.clarify_flag = resp.data.clarify_flag;
                $scope.compliance_flag = resp.data.compliance_flag;
                $scope.proceed_flag = resp.data.proceed_flag;
            });

            var url = 'api/IdasTrnLsaManagement/limitinfodtl';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.final_flag = resp.data.final_flag;

            });
        }
        $scope.compliance_submit = function (lsacreate_gid)
        {
            var params = {
                nach_mandate: $scope.rdbnach,
                sign_match: $scope.rdbsign,
                sign_match_kyc: $scope.rdbkyc_provided,
                escrow_opened: $scope.rdbescrow,
                appropriate_stamp: $scope.rdbstamp,
                roc_filling: $scope.rdbroc,
                cersai: $scope.rdbcersai,
                nach_mandate_remarks: $scope.txtnach_remarks,
                sign_match_remarks: $scope.txtsign_remarks,
                sign_match_kyc_remarks: $scope.txtsign_kyc_remarks,
                escrow_opened_remarks: $scope.txtescrow_remarks,
                appropriate_stamp_remarks: $scope.txtstamp_remarks,
                roc_filling_remarks: $scope.txtroc_remarks,
                cersai_remarks:$scope.txtcersai_remarks,
                lsacreate_gid: $scope.lsacreate_gid,
            };
     
            var url = 'api/IdasTrnLsaManagement/compliancecheck';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                 

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
                compliancechacklist();
            });

        }
        function compliancechacklist()
        {
            var params =
                     {
                         lsacreate_gid: $scope.lsacreate_gid,
                     }
            var url = 'api/IdasTrnLsaManagement/Getcompliancecheckinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.nach_mandate = resp.data.nach_mandate;
                $scope.sign_match = resp.data.sign_match;
                $scope.sign_match_kyc = resp.data.sign_match_kyc;
                $scope.escrow_opened = resp.data.escrow_opened;
                $scope.appropriate_stamp = resp.data.nach_mandate;
                $scope.roc_filling = resp.data.roc_filling;
                $scope.nach_mandate_remarks = resp.data.nach_mandate_remarks;
                $scope.sign_match_remarks = resp.data.sign_match_remarks;
                $scope.sign_match_kyc_remarks = resp.data.sign_match_kyc_remarks;
                $scope.escrow_opened_remarks = resp.data.escrow_opened_remarks;
                $scope.appropriate_stamp_remarks = resp.data.appropriate_stamp_remarks;
                $scope.roc_filling_remarks = resp.data.roc_filling_remarks;
                $scope.cersai = resp.data.cersai;
                $scope.cersai_remarks = resp.data.cersai_remarks;

            });
            var url = 'api/IdasTrnLsaManagement/Getlsainfo';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.document_charge_flag = resp.data.document_charge_flag;
                $scope.recover_flag = resp.data.recover_flag;
                $scope.customername = resp.data.customer_name;
                $scope.clarify_flag = resp.data.clarify_flag;
                $scope.compliance_flag = resp.data.compliance_flag;
                $scope.proceed_flag = resp.data.proceed_flag;
            });
            var url = 'api/IdasTrnLsaManagement/limitinfodtl';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.final_flag = resp.data.final_flag;

            });
        }
        $scope.proceed = function (lsacreate_gid) {
            var params = { 
                lsacreate_gid: $scope.lsacreate_gid,
            };
   
            var url = 'api/IdasTrnLsaManagement/proceed_finalinfo';
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
                    $state.go('app.lsaManagement');
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

        $scope.lsaback = function () {
            $state.go('app.lsaManagement');
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
            frm.append('document_type', $scope.document_type);
            frm.append('lsacreate_gid', $scope.lsacreate_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;

            var url = 'api/IdasTrnLsaManagement/Uploaddocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.UploadDocumentList;
             
                $("#addupload").val('');
                if (resp.data.status == true) {
                    var params = {
                        lsacreate_gid: $scope.lsacreate_gid,

                    };
                    var url = 'api/IdasTrnLsaManagement/Getdocument';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.filename_list = resp.data.UploadDocumentList;


                    });
                    unlockUI();
                    $scope.document_type = '';
                    $scope.showdiv = true;
                    $scope.hidediv = false;
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
            });
        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1; 
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
       
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.edit_limitinfo = function (val) {
            $scope.limitinfodtl_gid = val;
            $scope.limitinfodtl_gid = localStorage.setItem('limitinfodtl_gid', val);
     
            $state.go('app.idasTrnlimitInfoEdit');
        }

        $scope.editprocessing_fee = function (lsacreate_gid) {
            var lsacreate_gid = lsacreate_gid;
            var modalInstance = $modal.open({
                templateUrl: '/editprocessigninfo.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    lsacreate_gid: lsacreate_gid,

                };
                var url = 'api/IdasTrnLsaManagement/Getprocessingfeeinfo';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rdbrecovered_type = resp.data.recovered_type;
                    $scope.txtrecovered_amount = resp.data.recovered_amount;
                    $scope.txtcheque_no = resp.data.chequeno_details;
                    $scope.txtcheque_date = new Date(resp.data.cheque_date);
                    $scope.txtrecoveredbank_name = resp.data.processingfeebank_name;
                    $scope.txtrecoveredaccount_no = resp.data.processingfeaccount_name;
                    $scope.txtrecovered_remarks = resp.data.recover_remarks;
                    $scope.txtto_be_recoveredamount = resp.data.to_be_recoveredamount;
                  
                    if (resp.data.recovered_type == "To be Recovered") {
                        $scope.tobe_recovered = true;
                        $scope.already_recovered = false;
                    }
                    if (resp.data.recovered_type == "Already Recovered") {
                        $scope.tobe_recovered = false;
                        $scope.already_recovered = true;
                    }
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.recoveredstatus=function()
                {
                    if ($scope.rdbrecovered_type == "Already Recovered") {
                        $scope.tobe_recovered = false;
                        $scope.already_recovered = true;
                    }
                }
                $scope.recoveredstatus1 = function () {
                    if ($scope.rdbrecovered_type == "To be Recovered") {
                        $scope.tobe_recovered = true;
                        $scope.already_recovered = false;
                    }
                    
                }
            
                $scope.amountschange0=function()
                {
                    var input = document.getElementById('txtInput0').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');
                    var lseditrecoveredamount_words = inWords(str);
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    if (amount == 'NaN') {
                        Notify.alert('Accept Numeric Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtrecovered_amount = "";

                    }
                    else {
                        document.getElementById('editrecoveredamount_words').innerHTML = lseditrecoveredamount_words;
                        $scope.txtrecovered_amount = amount;
                    }
                }
                $scope.amountschange01 = function () {
                    var input = document.getElementById('txtInput01').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');

                    var lsedittoberecoveredamount_words = inWords(str);
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    if (amount == 'NaN') {
                        Notify.alert('Accept Numeric Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtto_be_recoveredamount = "";

                    }
                    else {
                        document.getElementById('edittoberecoveredamount_words').innerHTML = lsedittoberecoveredamount_words;
                        $scope.txtto_be_recoveredamount = amount;
                    }
                }
                $scope.processignfee_update = function () {

                    var params = {
                        recovered_type: $scope.rdbrecovered_type,
                        recovered_amount: $scope.txtrecovered_amount,
                        chequeno_details: $scope.txtcheque_no,
                        chequedate_details: $scope.txtcheque_date,
                        processingfeebank_name: $scope.txtrecoveredbank_name,
                        processingfeaccount_name: $scope.txtrecoveredaccount_no,
                        recover_remarks: $scope.txtrecovered_remarks,
                        lsacreate_gid: lsacreate_gid,
                        to_be_recoveredamount: $scope.txtto_be_recoveredamount,
                    }

                    var url = 'api/IdasTrnLsaManagement/updateprocessingfee';
                    SocketService.post(url, params).then(function (resp) {


                        if (resp.data.status == true) {
                            processingfeeslist();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                        
                    });


                }
            }
        }
        function processingfeeslist() {
            var params =
                       {
                           lsacreate_gid: $scope.lsacreate_gid,
                       }
            var url = 'api/IdasTrnLsaManagement/Getprocessingfeeinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.recovered_type = resp.data.recovered_type;
                $scope.recovered_amount = resp.data.recovered_amount;
                $scope.chequeno_details = resp.data.chequeno_details;
                $scope.chequedate_details = resp.data.chequedate_details;
                $scope.processingfeebank_name = resp.data.processingfeebank_name;
                $scope.processingfeaccount_name = resp.data.processingfeaccount_name;
                $scope.recover_remarks = resp.data.recover_remarks;
                $scope.to_be_recoveredamount = resp.data.to_be_recoveredamount;

            });
            var url = 'api/IdasTrnLsaManagement/Getlsainfo';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.document_charge_flag = resp.data.document_charge_flag;
                $scope.recover_flag = resp.data.recover_flag;
                $scope.customername = resp.data.customer_name;
                $scope.clarify_flag = resp.data.clarify_flag;
                $scope.compliance_flag = resp.data.compliance_flag;
                $scope.proceed_flag = resp.data.proceed_flag;
            });
            var url = 'api/IdasTrnLsaManagement/limitinfodtl';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.final_flag = resp.data.final_flag;

            });
        }
        $scope.editdocumentcharges = function (lsacreate_gid) {
            var lsacreate_gid = lsacreate_gid;
            var modalInstance = $modal.open({
                templateUrl: '/editdocumentcharge.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
          
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    lsacreate_gid: lsacreate_gid,

                };
                var url = 'api/IdasTrnLsaManagement/Getdocumentchargeinfo';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtdoc_recovered_amount = resp.data.doc_recovered_amount;
                    $scope.txtdoc_cheque_no = resp.data.doc_chequeno_details;
                    $scope.txtdoc_cheque_date = new Date(resp.data.doc_cheque_date);
                    $scope.txtdoc_bank_name = resp.data.doc_feebank_name;
                    $scope.txtdoc_account_no = resp.data.doc_feaccount_name;
                    $scope.txtdocument_charges_edit = new Intl.NumberFormat('en-IN').format(Number(resp.data.document_charge)) + ".00";
                    $scope.txtgst_edit = resp.data.document_charge_gst;
                    $scope.rdbdocumentcharge = resp.data.documentcharge_applicable;
                    $scope.lbldocumentcharge_applicable = resp.data.documentcharge_applicable;
                    $scope.txtdocumentcharge_remarks = resp.data.documentcharge_remarks;
                    if (resp.data.documentcharge_applicable == "Yes") {
                        $scope.documentcharge_yes = true;
                        $scope.documentcharge_no = false;
                    }
                    if (resp.data.documentcharge_applicable == "No") {
                        $scope.documentcharge_yes = false;
                        $scope.documentcharge_no = true;
                    }
                });
             
                vm.calender3 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    vm.open3 = true;
                };
                $scope.documentchargestatus_yes=function()
                {
                    $scope.documentcharge_yes = true;
                    $scope.documentcharge_no = false;
                }
                $scope.documentchargestatus_no=function()
                {
                    $scope.documentcharge_yes = false;
                    $scope.documentcharge_no = true;
                }
                $scope.documentchargesedit = function () {
                    var input = document.getElementById('documentcharge_edit').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');
                    var lseditdocumentamount_words = inWords(str);
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    $scope.txtdocument_charges_edit = amount;

                    document.getElementById('editdocumentamount_words').innerHTML = lseditdocumentamount_words;

                    if ($scope.txtgst_edit != null) {
                        var amount1 = (($scope.txtdocument_charges_edit.replace(/[\s,]+/g, '').trim()) * ($scope.txtgst_edit) / 100);
                        var doc = ($scope.txtdocument_charges_edit.replace(/[\s,]+/g, '').trim());
                        var output1 = (parseInt(amount1) + parseInt(doc));

                        $scope.txtdoc_recovered_amount = new Intl.NumberFormat('en-IN').format(Number(output1));
                    }

                }
                $scope.gstedit = function () {
                    var amount = (($scope.txtdocument_charges_edit.replace(/[\s,]+/g, '').trim()) * ($scope.txtgst_edit) / 100);
                    var doc = ($scope.txtdocument_charges_edit.replace(/[\s,]+/g, '').trim());
                    var output = (parseInt(amount) + parseInt(doc));
                    var lseditdocumentrecoveredamount_words = inWords(str);
                    document.getElementById('editdocumentrecoveredamount_words').innerHTML = lseditdocumentrecoveredamount_words;
                    $scope.txtdoc_recovered_amount = new Intl.NumberFormat('en-IN').format(Number(output));

                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
                $scope.amountschange02 = function () {
                    var input = document.getElementById('txtInput02').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');
                    var lseditdocumentrecoveredamount_words = inWords(str);
                    document.getElementById('editdocumentrecoveredamount_words').innerHTML = lseditdocumentrecoveredamount_words;
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    $scope.txtdoc_recovered_amount = amount;
                }
           
                $scope.documentcharges_update = function () {
                   
                    var params = {
                        doc_recovered_amount: $scope.txtdoc_recovered_amount,
                        doc_chequeno_details: $scope.txtdoc_cheque_no,
                        doc_chequedate_details: $scope.txtdoc_cheque_date,
                        doc_feebank_name: $scope.txtdoc_bank_name,
                        doc_feaccount_name: $scope.txtdoc_account_no,
                        lsacreate_gid: lsacreate_gid,
                        document_charge: $scope.txtdocument_charges_edit,
                        document_charge_gst: $scope.txtgst_edit,
                        documentcharge_remarks: $scope.txtdocumentcharge_remarks,
                        documentcharge_applicable: $scope.rdbdocumentcharge
                    }

                   
                 
                    var url = 'api/IdasTrnLsaManagement/updatedocumentcharges';
                    SocketService.post(url, params).then(function (resp) {


                        if (resp.data.status == true) {
                            documentcharges();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                      
                    });


                }
            }
        }
        function documentcharges() {
            var params =
                       {
                           lsacreate_gid: $scope.lsacreate_gid,
                       }
            var url = 'api/IdasTrnLsaManagement/Getdocumentchargeinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.doc_recovered_amount = resp.data.doc_recovered_amount;
                $scope.doc_chequeno_details = resp.data.doc_chequeno_details;
                $scope.doc_chequedate_details = resp.data.doc_chequedate_details;
                $scope.doc_feebank_name = resp.data.doc_feebank_name;
                $scope.doc_feaccount_name = resp.data.doc_feaccount_name;
                $scope.document_name = resp.data.document_name;
                $scope.document_path = resp.data.document_path;
                $scope.document_charge = new Intl.NumberFormat('en-IN').format(Number(resp.data.document_charge)) + ".00";
                $scope.document_charge_gst = resp.data.document_charge_gst + "%";
                if (resp.data.document_name == null) {
                    $scope.document = false;
                }
                else {
                    $scope.document = true;
                }
                $scope.lbldocumentcharge_applicable = resp.data.documentcharge_applicable;
                $scope.lbldocumentcharge_remarks = resp.data.documentcharge_remarks;
            });
            var url = 'api/IdasTrnLsaManagement/Getlsainfo';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.document_charge_flag = resp.data.document_charge_flag;
                $scope.recover_flag = resp.data.recover_flag;
                $scope.customername = resp.data.customer_name;
                $scope.clarify_flag = resp.data.clarify_flag;
                $scope.compliance_flag = resp.data.compliance_flag;
                $scope.proceed_flag = resp.data.proceed_flag;
            });
            var url = 'api/IdasTrnLsaManagement/limitinfodtl';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.final_flag = resp.data.final_flag;

            });
        }
        $scope.processingfee_clear=function()
        {
            $scope.rdbrecovered_type = '';
            $scope.txtrecovered_amount = '';
            $scope.txtcheque_no = '';
            $scope.txtcheque_date = '';
            $scope.txtrecoveredbank_name = '';
            $scope.txtrecoveredaccount_no = '';
            $scope.txtto_be_recoveredamount = '';
            $scope.txtrecovered_remarks = '';
        }
        $scope.documentcharge_clear=function()
        {
            $scope.txtdoc_recovered_amount = '';
            $scope.txtdoc_cheque_no = '';
            $scope.txtdoc_cheque_date = '';
            $scope.txtdoc_bank_name = '';
            $scope.txtdoc_account_no = '';
            $scope.txtdocument_charges = '';
            $scope.txtgst = '';
            $scope.txtdocumentcharge_remarks = '';
        }

        $scope.addsecurityinfo = function (lsacreate_gid) {
            $scope.lsacreate_gid = localStorage.setItem('lsacreate_gid', lsacreate_gid);
   
            $state.go('app.IdasMstSecurityAdd');
          
        }
        $scope.delete_securityinfo = function (val, data) {
            var params = { collateral_gid: val };

            var url = 'api/IdasTrnLsaManagement/securityinfo_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var params =
                       {
                           lsacreate_gid: $scope.lsacreate_gid,
                       }
                    var url = 'api/IdasTrnLsaManagement/Getsanction2Colletarl';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.customer2security_list = resp.data.customersecurity_list;

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

        $scope.edit_security = function (collateral_gid)
        {

            $scope.collateral_gid = localStorage.setItem('collateral_gid', collateral_gid);

            $state.go('app.IdasTrnSecurityEdit');
           
        }
        
    
    $scope.documentchargestatus_yes = function () {
        console.log($scope.rdbdocumentcharge);
        if ($scope.rdbdocumentcharge == "Yes") {
            $scope.Yes = false;
            $scope.No = true;
        }
    }
    $scope.documentchargestatus_no = function () {
     
        if ($scope.rdbdocumentcharge == "No") {
            $scope.Yes = true;
            $scope.No = false;
        }
    }
    $scope.documentchargestatus = function ()
    {
        if ($scope.txtdocumentcharge_remarks == undefined)
        {
            var remarks = 'NA';
        }
        else {
            var remarks = $scope.txtdocumentcharge_remarks;
        }
        var params = {
           
            documentcharge_applicable: $scope.rdbdocumentcharge,            
            documentcharge_remarks: remarks,
            lsacreate_gid: $scope.lsacreate_gid,                    
        };    
        var url = 'api/IdasTrnLsaManagement/documentchargeapplicable';
        lockUI()
        SocketService.post(url, params).then(function (resp) {
            if (resp.data.status == true) {
                unlockUI()
                
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
            documentcharges();
        });
       
    }
    $scope.proceed_approval=function()
    {
        var params = {

            lsacreate_gid: $scope.lsacreate_gid,

        };
        var url = 'api/IdasTrnLsaManagement/LSAapproval';
        lockUI()
        SocketService.post(url, params).then(function (resp) {
            if (resp.data.status == true) {
                unlockUI()

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                $state.go('app.lsaManagement');
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
})();
