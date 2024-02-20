(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstCreateSanction', idasMstCreateSanction);

    idasMstCreateSanction.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function idasMstCreateSanction($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasMstCreateSanction';
        var vertical_gid;
        var vertical_code;
        activate();

        function activate() {
            $scope.colandingyes = false;
            $scope.colandingyes = false;
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.mandatorycolending = false;

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            $scope.customer_pnl = false;
            var url = 'api/entity/Entity';

            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;

            });
          
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;

            });
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });
            var url = 'api/IdasTrnLsaManagement/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });
            var url = 'api/IdasMstSanction/tempdelete';
            SocketService.get(url).then(function (resp) {
            });

        }
        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }


                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Enter atleast three character";
            }
        }
        $scope.fillTextbox = function (customer_gid, customer_name) {
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;


            var params = {
                customer_gid: customer_gid
            }


            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_pnl = true;
                $scope.mdlheadsofcustomer = resp.data;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.vertical_code = resp.data.vertical_code;
                vertical_gid = resp.data.vertical_gid;
                vertical_code = resp.data.vertical_code;

            });
        }
       
        $scope.rdbcolanding_yes = function () {
            $scope.mandatorycolending = false;
            $scope.colandingyes = true;

        }
        $scope.rdbcolanding_no = function () {
            $scope.mandatorycolending = false;
            $scope.colandingyes = false;

        }

        $scope.rdbdeclaration_yes = function () {
            $scope.esdeclarationyes = true;
            $scope.esdeclarationno = false;
        }
        $scope.rdbdeclaration_no = function () {
            $scope.esdeclarationyes = false;
            $scope.esdeclarationno = true;
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


        $scope.amountschange = function () {

            var input = document.getElementById('txtInput').value;
            var str = input.replace(/,/g, '');

            var output = Number(str).toLocaleString('en-IN');
            var lswords_requestedloan = inWords(str);


            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtSanctionAmount = "";

            }
            else {
                document.getElementById('words_requestedloan').innerHTML = lswords_requestedloan;

                $scope.txtSanctionAmount = output;
                $scope.sanction_validation = false;
            }
        }
        $scope.ngclickevent = function () {
            $scope.mandatorycolending = false;
        }

        $scope.sanctionSubmit = function () {
            var params = {
                esdeclaration_status: $scope.rdbdeclaration,
            }
            var url = 'api/IdasMstSanction/mandatoryfile_check';
            SocketService.getparams(url,params).then(function (resp) {
                if (resp.data.status == true) {
                    if ($scope.rdbcolanding == "No") {
                        if (vertical_code == 'FPO')
                        {
                            if (($scope.rdbpaycard == "") || ($scope.rdbpaycard == undefined))
                            {
                                Notify.alert("Kindly Select Paycard value");
                            }
                            else {

                           
                        $scope.mandatorycolending = false;
                        var input = $scope.txtSanctionAmount;
                        var arr = input.split(',');
                        var i;
                        for (i = 0; i < arr.length; i++) {
                            var str = input.replace(',', '');
                            input = str;
                        }
                        var zonal_name = $('#zonal_name :selected').text();
                        var businesshead_name = $('#businesshead_name :selected').text();
                        var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
                        var cluster_manager_name = $('#cluster_manager_name :selected').text();
                        var creditmgmt_name = $('#creditmanager_name :selected').text();

                        var params = {
                            sanction_refno: $scope.sanctionrefno,
                            sanction_amount: input,
                            sanction_date: $scope.txtSanctionDate,
                            customername: $scope.customer,
                            customer_gid: $scope.customer_gid,
                            vertical_gid: vertical_gid,
                            vertical_code: vertical_code,
                            zonal_name: zonal_name,
                            businesshead_name: businesshead_name,
                            relationshipmgmt_name: relationshipmgmt_name,
                            cluster_manager_name: cluster_manager_name,
                            creditmanager_name: creditmgmt_name,
                            zonalGid: $scope.zonalHead,
                            businessHeadGid: $scope.businessHead,
                            relationshipMgmtGid: $scope.relationshipMgmt,
                            clustermanagerGid: $scope.clustermanager,
                            creditmanagerGid: $scope.creditmgmt_name,
                            sanction_branch_name: $scope.branch_gid.branch_name,
                            sanction_state_name: $scope.state_gid.state_name,
                            sanction_branch_gid: $scope.branch_gid.branch_gid,
                            sanction_state_gid: $scope.state_gid.state_gid,
                            colanding_status: $scope.rdbcolanding,
                            colander_name: $scope.txtcolander_name,
                            entity: $scope.cboentity_type.entity_name,
                            entity_gid: $scope.cboentity_type.entity_gid,
                            paycard: $scope.rdbpaycard,
                            esdeclaration_status: $scope.rdbdeclaration,
                        }
                        
                        var url = 'api/IdasMstSanction/CreateSanction';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();

                                Notify.alert(resp.data.message, 'success')
                                $state.go('app.idasMstSanctionSummary');

                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message)
                            }
                            activate();
                        });
                            }
                        }
                        else {
                            $scope.mandatorycolending = false;
                            var input = $scope.txtSanctionAmount;
                            var arr = input.split(',');
                            var i;
                            for (i = 0; i < arr.length; i++) {
                                var str = input.replace(',', '');
                                input = str;
                            }

                            // var customer_name = $('#customername :selected').text();
                            var zonal_name = $('#zonal_name :selected').text();
                            var businesshead_name = $('#businesshead_name :selected').text();
                            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
                            var cluster_manager_name = $('#cluster_manager_name :selected').text();
                            var creditmgmt_name = $('#creditmanager_name :selected').text();

                            var params = {
                                sanction_refno: $scope.sanctionrefno,
                                sanction_amount: input,
                                sanction_date: $scope.txtSanctionDate,
                                customername: $scope.customer,
                                customer_gid: $scope.customer_gid,
                                vertical_gid: vertical_gid,
                                vertical_code: vertical_code,
                                zonal_name: zonal_name,
                                businesshead_name: businesshead_name,
                                relationshipmgmt_name: relationshipmgmt_name,
                                cluster_manager_name: cluster_manager_name,
                                creditmanager_name: creditmgmt_name,
                                zonalGid: $scope.zonalHead,
                                businessHeadGid: $scope.businessHead,
                                relationshipMgmtGid: $scope.relationshipMgmt,
                                clustermanagerGid: $scope.clustermanager,
                                creditmanagerGid: $scope.creditmgmt_name,
                                sanction_branch_name: $scope.branch_gid.branch_name,
                                sanction_state_name: $scope.state_gid.state_name,
                                sanction_branch_gid: $scope.branch_gid.branch_gid,
                                sanction_state_gid: $scope.state_gid.state_gid,
                                colanding_status: $scope.rdbcolanding,
                                colander_name: $scope.txtcolander_name,
                                entity: $scope.cboentity_type.entity_name,
                                entity_gid: $scope.cboentity_type.entity_gid,
                                paycard: $scope.rdbpaycard,
                                esdeclaration_status: $scope.rdbdeclaration,
                            }
                            var url = 'api/IdasMstSanction/CreateSanction';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    unlockUI();

                                    Notify.alert(resp.data.message, 'success')
                                    $state.go('app.idasMstSanctionSummary');

                                }
                                else {
                                    unlockUI();
                                    Notify.alert(resp.data.message)
                                }
                                activate();
                            });
                        }
                    }
                    else {
                        if (vertical_code == 'FPO') {
                            if (($scope.rdbpaycard == "") || ($scope.rdbpaycard == undefined)) {
                                Notify.alert("Kindly Select Paycard value");
                            }
                            else {
                                if (($scope.txtcolander_name == "") || ($scope.txtcolander_name == undefined)) {
                                    $scope.mandatorycolending = true;
                                }
                                else {
                                    $scope.mandatorycolending = false;
                                    var input = $scope.txtSanctionAmount;
                                    var arr = input.split(',');
                                    var i;
                                    for (i = 0; i < arr.length; i++) {
                                        var str = input.replace(',', '');
                                        input = str;
                                    }

                                    // var customer_name = $('#customername :selected').text();
                                    var zonal_name = $('#zonal_name :selected').text();
                                    var businesshead_name = $('#businesshead_name :selected').text();
                                    var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
                                    var cluster_manager_name = $('#cluster_manager_name :selected').text();
                                    var creditmgmt_name = $('#creditmanager_name :selected').text();

                                    var params = {
                                        sanction_refno: $scope.sanctionrefno,
                                        sanction_amount: input,
                                        sanction_date: $scope.txtSanctionDate,
                                        customername: $scope.customer,
                                        customer_gid: $scope.customer_gid,
                                        vertical_gid: vertical_gid,
                                        vertical_code: vertical_code,
                                        zonal_name: zonal_name,
                                        businesshead_name: businesshead_name,
                                        relationshipmgmt_name: relationshipmgmt_name,
                                        cluster_manager_name: cluster_manager_name,
                                        creditmanager_name: creditmgmt_name,
                                        zonalGid: $scope.zonalHead,
                                        businessHeadGid: $scope.businessHead,
                                        relationshipMgmtGid: $scope.relationshipMgmt,
                                        clustermanagerGid: $scope.clustermanager,
                                        creditmanagerGid: $scope.creditmgmt_name,
                                        sanction_branch_name: $scope.branch_gid.branch_name,
                                        sanction_state_name: $scope.state_gid.state_name,
                                        sanction_branch_gid: $scope.branch_gid.branch_gid,
                                        sanction_state_gid: $scope.state_gid.state_gid,
                                        colanding_status: $scope.rdbcolanding,
                                        colander_name: $scope.txtcolander_name,
                                        entity: $scope.cboentity_type.entity_name,
                                        entity_gid: $scope.cboentity_type.entity_gid,
                                        paycard: $scope.rdbpaycard,
                                        esdeclaration_status: $scope.rdbdeclaration,
                                    }
                                    var url = 'api/IdasMstSanction/CreateSanction';
                                    lockUI();
                                    SocketService.post(url, params).then(function (resp) {
                                        if (resp.data.status == true) {
                                            unlockUI();

                                            Notify.alert(resp.data.message, 'success')
                                            $state.go('app.idasMstSanctionSummary');

                                        }
                                        else {
                                            unlockUI();
                                            Notify.alert(resp.data.message)
                                        }
                                        activate();
                                    });
                                }
                            }
                        }
                        else {
                            if (($scope.txtcolander_name == "") || ($scope.txtcolander_name == undefined)) {
                                $scope.mandatorycolending = true;
                            }
                            else {
                                $scope.mandatorycolending = false;
                                var input = $scope.txtSanctionAmount;
                                var arr = input.split(',');
                                var i;
                                for (i = 0; i < arr.length; i++) {
                                    var str = input.replace(',', '');
                                    input = str;
                                }

                                // var customer_name = $('#customername :selected').text();
                                var zonal_name = $('#zonal_name :selected').text();
                                var businesshead_name = $('#businesshead_name :selected').text();
                                var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
                                var cluster_manager_name = $('#cluster_manager_name :selected').text();
                                var creditmgmt_name = $('#creditmanager_name :selected').text();

                                var params = {
                                    sanction_refno: $scope.sanctionrefno,
                                    sanction_amount: input,
                                    sanction_date: $scope.txtSanctionDate,
                                    customername: $scope.customer,
                                    customer_gid: $scope.customer_gid,
                                    vertical_gid: vertical_gid,
                                    vertical_code: vertical_code,
                                    zonal_name: zonal_name,
                                    businesshead_name: businesshead_name,
                                    relationshipmgmt_name: relationshipmgmt_name,
                                    cluster_manager_name: cluster_manager_name,
                                    creditmanager_name: creditmgmt_name,
                                    zonalGid: $scope.zonalHead,
                                    businessHeadGid: $scope.businessHead,
                                    relationshipMgmtGid: $scope.relationshipMgmt,
                                    clustermanagerGid: $scope.clustermanager,
                                    creditmanagerGid: $scope.creditmgmt_name,
                                    sanction_branch_name: $scope.branch_gid.branch_name,
                                    sanction_state_name: $scope.state_gid.state_name,
                                    sanction_branch_gid: $scope.branch_gid.branch_gid,
                                    sanction_state_gid: $scope.state_gid.state_gid,
                                    colanding_status: $scope.rdbcolanding,
                                    colander_name: $scope.txtcolander_name,
                                    entity: $scope.cboentity_type.entity_name,
                                    entity_gid: $scope.cboentity_type.entity_gid,
                                    paycard: $scope.rdbpaycard,
                                    esdeclaration_status: $scope.rdbdeclaration,
                                }
                                var url = 'api/IdasMstSanction/CreateSanction';
                                lockUI();
                                SocketService.post(url, params).then(function (resp) {
                                    if (resp.data.status == true) {
                                        unlockUI();

                                        Notify.alert(resp.data.message, 'success')
                                        $state.go('app.idasMstSanctionSummary');

                                    }
                                    else {
                                        unlockUI();
                                        Notify.alert(resp.data.message)
                                    }
                                    activate();
                                });
                            }
                        }
                    }
                }
                else {
                    Notify.alert(resp.data.message, 'warning')
                }
            });
        }


        $scope.sanctionback = function (val) {
            $state.go('app.idasMstSanctionSummary');
        }

        $scope.importExcel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/excelImport.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    $("#excelImport").val('');
                };


                $scope.upload = function (val, val1, name) {
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('uploadtype', $scope.cboexcel_type);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;

                }

                $scope.uploadexcelclick = function () {
                    lockUI();
                    var url = "api/IdasMstSanction/postexcelupload";
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $("#excelImport").val('');

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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        unlockUI();
                    });
                }

            }
        }
        //Delete CAM Document
        $scope.deleteCAM = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/IdasMstSanction/camdoc_delete_add';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.CAMfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.CAMfilename_list.splice(key, 1);
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

        //Delete MOM Document
        $scope.deleteMOM = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/IdasMstSanction/momdoc_delete_add';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.MOMfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.MOMfilename_list.splice(key, 1);
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
        // Delete the sanction letter
        $scope.document_cancelclick = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/IdasMstSanction/sanctionletter_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.SANfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.SANfilename_list.splice(key, 1);
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

        $scope.uploadclick = function () {
            $scope.uploadddiv = true;
            $scope.uploaddclickdiv = false;
        }

        $scope.cancelupload = function () {
            $scope.uploadddiv = false;
            $scope.uploaddclickdiv = true;
            $("#addupload").val('');
        }
        $scope.uploadclickMOM = function () {
            $scope.uploadMOMdiv = true;
            $scope.uploadMOMclickdiv = false;
        }

        $scope.cancelMOMupload = function () {
            $scope.uploadMOMdiv = false;
            $scope.uploadMOMclickdiv = true;
            $("#addupload").val('');
        }
        $scope.uploadCAM_doc = function (val, val1, name) {
            if (($scope.CAMdocument_type == null) || ($scope.CAMdocument_type == '') || ($scope.CAMdocument_type == undefined)) {
                $("#addCAMupload").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
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
                frm.append('document_type', $scope.CAMdocument_type);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasMstSanction/EditCAMddocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#addCAMupload").val('');
                    unlockUI();
                    if (resp.data.status == true) {

                        $scope.CAMdocument_type = '';
                        $scope.showdiv = true;
                        $scope.hidediv = false;

                        var url = 'api/IdasMstSanction/Getcamdocmentadd';
                        SocketService.get(url).then(function (resp) {
                            $scope.CAMfilename_list = resp.data.UploadCOMDocumentList;
                        });
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
        }
        $scope.uploadMOM_doc = function (val, val1, name) {
            if (($scope.MOMdocument_type == null) || ($scope.MOMdocument_type == '') || ($scope.MOMdocument_type == undefined))
            {
                $("#addMOMupload").val('');
                 Notify.alert('Kindly Enter the Document Title', 'warning');
           }
            else {
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
                frm.append('document_type', $scope.MOMdocument_type);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasMstSanction/EditMOMddocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    if (resp.data.status == true) {
                        $("#addMOMupload").val('');
                        unlockUI();
                        $scope.MOMdocument_type = '';
                        $scope.showdiv = true;
                        $scope.hidediv = false;

                        var url = 'api/IdasMstSanction/Getmomdocmentadd';
                        SocketService.get(url).then(function (resp) {
                            $scope.MOMfilename_list = resp.data.UploadMOMDocumentList;
                        });
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!', {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
        }
    }
        //Upload Sanction Letter
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_type', $scope.SANdocument_type);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;

            var url = 'api/IdasMstSanction/Uploadsanctionletter';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#addSANupload").val('');
              
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.SANdocument_type = '';
                    $scope.showdiv = true;
                    $scope.hidediv = false;
                    var url = 'api/IdasMstSanction/Getsanctionletter';
                    SocketService.get(url).then(function (resp) {
                        $scope.SANfilename_list = resp.data.UploadSANDocumentList;
                    });
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
            });
        }
        $scope.downloadsCAM = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

        }

        $scope.downloadsMOM = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }

        $scope.downloadsanctionletter = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }

   //Upload es_declaration available document
        $scope.uploades_declaration = function (val, val1, name) {
            if (($scope.es_declarationdocument_type == null) || ($scope.es_declarationdocument_type == '') || ($scope.es_declarationdocument_type == undefined)) {
                $("#adduploades_declaration").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
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
                frm.append('document_type', $scope.es_declarationdocument_type)
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;

                var url = 'api/IdasMstSanction/Uploades_declarationdocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    if (resp.data.status == true) {
                        $("#adduploades_declaration").val('');
                        unlockUI();
                        $scope.es_declarationdocument_type = '';

                        var url = 'api/IdasMstSanction/Getesdocument';
                        SocketService.get(url).then(function (resp) {
                            $scope.uploadesfilename_list = resp.data.UploadES_DocumentList;
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

        $scope.esdownloaddocument = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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
        }

        // Delete the Normal Document
        $scope.esdocument_cancelclick = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/IdasMstSanction/uploadesdocumentadd_delete';
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
                frm.append('document_type', $scope.deviationmaildocument_type);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;

                var url = 'api/IdasMstSanction/Uploadmaildocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    if (resp.data.status == true) {
                        $("#addmailupload").val('');
                        unlockUI();
                        $scope.deviationmaildocument_type = '';
                        
                        var url = 'api/IdasMstSanction/GetMaildocument';
                        SocketService.get(url).then(function (resp) {
                            $scope.mailfilename_list = resp.data.DeviationMail_DocumentList;
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

        $scope.downloadmail = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }

        $scope.maildocument_cancelclick = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/IdasMstSanction/Maildocumentadd_delete';
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
    }
})();
