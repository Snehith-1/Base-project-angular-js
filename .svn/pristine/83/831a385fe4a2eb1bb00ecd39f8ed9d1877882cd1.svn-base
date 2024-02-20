(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstInsuranceCompanyAddController', AgrMstInsuranceCompanyAddController);

    AgrMstInsuranceCompanyAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstInsuranceCompanyAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstInsuranceCompanyAddController';



        activate();

        function activate() {
            

            vm.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened1 = true;
            };

            vm.open2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened2 = true;
            };

            vm.open3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened3 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/AgrMstSamAgroMaster/GetInsuranceCompanyTempClear';
            SocketService.get(url).then(function (resp) {
            });

        }

        $scope.premiumpaymentstatuschange = function(rdbpremiumpayment_status) {
            if(rdbpremiumpayment_status == 'Yes') {
                $scope.paiddate_visible = true;
            } else {
                $scope.paiddate_visible = false;
            }
        }


        $scope.policy_delete = function (insurancecompany2policy_gid) {
            var params =
                {
                    insurancecompany2policy_gid: insurancecompany2policy_gid
                }
            var url = 'api/AgrMstSamAgroMaster/DeletePolicy';
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                policylist();
            });

        }

        $scope.insurancecompany_submit = function () {
           
            var params = {
                insurancecompany_name: $scope.txtinsurancecompany_name,
            }
        var url = 'api/AgrMstSamAgroMaster/InsuranceCompanySubmit';
        lockUI();
        SocketService.post(url, params).then(function (resp) {
            if (resp.data.status == true) {
                unlockUI();

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });          
                $location.url('app/AgrMstInsuranceCompany');  
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

       

    

       
        $scope.Back = function ()
        {
            $location.url('app/AgrMstInsuranceCompany');
        }
        $scope.policy_add = function () {
            if (($scope.txtpolicy_name == null || $scope.txtpolicy_name == undefined || $scope.txtpolicy_name == '') || ($scope.txtpolicy_number == null || $scope.txtpolicy_number == undefined || $scope.txtpolicy_number == '') ||
            ($scope.txtpolicy_amount == null || $scope.txtpolicy_amount == undefined || $scope.txtpolicy_amount == '') || ($scope.dtpolicyperiod_from == null || $scope.dtpolicyperiod_from == undefined || $scope.dtpolicyperiod_from == '') || 
            ($scope.dtpolicyperiod_to == null || $scope.dtpolicyperiod_to == undefined || $scope.dtpolicyperiod_to == '') || ($scope.txtpremium_amount == null || $scope.txtpremium_amount == undefined || $scope.txtpremium_amount == '') ||
            ($scope.rdbpremiumpayment_status == null || $scope.rdbpremiumpayment_status == undefined || $scope.rdbpremiumpayment_status == '') || ($scope.rdbpremiumpayment_status == 'Yes' && ($scope.dtpaid_date == null || $scope.dtpaid_date == undefined || $scope.dtpaid_date == ''))
            ) {
                Notify.alert("Kindly fill all the mandatory fields..!",'warning');
            }
            else if($scope.dtpolicyperiod_to < $scope.dtpolicyperiod_from) {
                Notify.alert("Policy Period To should be later than Policy Period From..!",'warning');
            } 
            else {
                var params = {
                    policy_name: $scope.txtpolicy_name,
                    policy_number: $scope.txtpolicy_number,
                    policy_amount: $scope.txtpolicy_amount.replaceAll(',',''),
                    policyperiod_from: $scope.dtpolicyperiod_from,
                    policyperiod_to: $scope.dtpolicyperiod_to,
                    premium_amount: $scope.txtpremium_amount.replaceAll(',',''),
                    premiumpayment_status: $scope.rdbpremiumpayment_status,
                    paid_date: $scope.dtpaid_date
                }
            var url = 'api/AgrMstSamAgroMaster/PostPolicyAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

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
                policylist();
                $scope.txtpolicy_name = '';
                $scope.txtpolicy_number = '';
                $scope.txtpolicy_amount = '';
                $scope.dtpolicyperiod_from = '';
                $scope.dtpolicyperiod_to = '';
                $scope.txtpremium_amount = '';
                $scope.rdbpremiumpayment_status = '';
                $scope.dtpaid_date = '';
                document.getElementById('words_policyamount').innerHTML = '';
                document.getElementById('words_premiumamount').innerHTML = '';
            });
            }
                
        
        }    

        function policylist() {
            var url = 'api/AgrMstSamAgroMaster/GetPolicyList';
            SocketService.get(url).then(function (resp) {
                $scope.policy_list = resp.data.policy_list;
            });
        }

         $scope.policy_view = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/PolicyView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                $scope.lblpolicy_name = data.policy_name;
                $scope.lblpolicy_number = data.policy_number;
                $scope.lblpolicy_amount = data.policy_amount;
                $scope.lblpolicyperiod_from = data.policyperiod_from;
                $scope.lblpolicyperiod_to = data.policyperiod_to;
                $scope.lblpremium_amount = data.premium_amount;
                $scope.lblpremiumpayment_status = data.premiumpayment_status;
                $scope.lblpaid_date = data.paid_date;
                

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.txtpolicyamount = function () {
            var input = document.getElementById('policyamount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_policyamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtvalue = "";
            }
            else {
                $scope.txtpolicy_amount = output;
                document.getElementById('words_policyamount').innerHTML = lswords_policyamount;
                 }   
        }

        $scope.txtpremiumamount = function () {
            var input = document.getElementById('premiumamount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_premiumamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtvalue = "";
            }
            else {
                $scope.txtpremium_amount = output;
                document.getElementById('words_premiumamount').innerHTML = lswords_premiumamount;
                 }   
        }

        
        $scope.policydoc_upload = function (insurancecompany2policy_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/policy_docupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
               policydoclist();

                $scope.policydocumentupload = function (val) {
                    if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                        $("#momdocument").val('');
                        Notify.alert('Kindly Enter the Document Title', 'warning');
                    } 
                    else {
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
                        frm.append('document_title', $scope.txtdocument_title);
                        frm.append('insurancecompany2policy_gid', insurancecompany2policy_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                        if ($scope.uploadfrm != undefined) {
                            lockUI();
                            var url = 'api/AgrMstSamAgroMaster/PolicyDocumentUpload';
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                               // $scope.policydoc_list = resp.data.policydoc_list;
                                unlockUI();
                                $scope.txtdocument_title = '';
                                $("#policydocupload").val('');
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
                                policydoclist();
                                unlockUI();
                            });
                        }
                        else {
                            alert('Document is not Available..!');
                            return;
                        }
                    }
                }

                $scope.policydoc_delete = function (insurancecompanypolicy2document_gid) {
                    lockUI();
                    var params = {
                        insurancecompanypolicy2document_gid: insurancecompanypolicy2document_gid,
                    }
                    var url = 'api/AgrMstSamAgroMaster/PolicyDocumentDelete';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        policydoclist();
                        unlockUI();
                    });
                }

                function policydoclist() {
                    var params = {
                        insurancecompany2policy_gid: insurancecompany2policy_gid,
                    }
                    var url = 'api/AgrMstSamAgroMaster/PolicyDocumentUploadTmpList';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.policydoc_list = resp.data.policydoc_list;
    
                    });
    
                }

                $scope.download_doc = function (val1, val2) {

                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');

                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.policydoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.policydoc_list[i].document_path, $scope.policydoc_list[i].document_name);
                    }
                }            

            }
        }
        
        $scope.futuredatecheck = function (val) {
                var params = {
                    date: val.toDateString()
                }
                var url = 'api/AgrMstApplicationAdd/FutureDateCheck';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == false) {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });   
        } 

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.policydoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.policydoc_list[i].document_path, $scope.policydoc_list[i].document_name);
            }
        }
       
    }
})();

