(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lawyerPaymentViewcontroller', lawyerPaymentViewcontroller);

    lawyerPaymentViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function lawyerPaymentViewcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'lawyerPaymentViewcontroller';

        activate();

        function activate() {
            $scope.lawyerinvoicedtl_gid = localStorage.getItem('lawyerinvoicedtl_gid')
            var params = {
                lawyerinvoicedtl_gid: localStorage.getItem('lawyerinvoicedtl_gid')
            }
            var url = "api/LawyerInvoice/getinvoicedetails";
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp);
                $scope.invoicedetail = resp.data;
                $scope.filename_list = resp.data.uploaddocument
            });

        }

        //$scope.downloads = function (val1, val2) {
         
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    var name = val2.split(".")
        //    link.download = name[0];
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
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
        $scope.cancel = function () {
            $state.go('app.LglTrnInvoiceSummary');
        }
        $scope.updatestatus = function () {
            var modalInstance = $modal.open({
                templateUrl: '/statusupdation.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params =
                    {
                        lawyerinvoicedtl_gid: localStorage.getItem('lawyerinvoicedtl_gid')
                    }
                console.log(params);
                lockUI();
                var url = 'api/LawyerInvoice/getinvoicedetails';

                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.service_type = resp.data.servicetype_gid;
                    $scope.caseref_no = resp.data.caseref_gid;
                    $scope.invoiceref_no = resp.data.invoice_refno;
                    $scope.case_type = resp.data.case_type;
                    $scope.invoice_amount = resp.data.invoice_amount;
                    $scope.invoice_date = resp.data.invoice_date;
                    $scope.servicerender_date = resp.data.servicerender_date;
                    $scope.invoice_remarks = resp.data.invoice_remarks;
                    console.log(resp.data.invoice_refno)
                });
                $scope.submit = function () {

                    var url = 'api/LawyerInvoice/updateinvoicestatus';
                    lockUI();
                    var params = {
                        lawyerinvoicedtl_gid: localStorage.getItem('lawyerinvoicedtl_gid'),
                        invoice_status: $scope.cbostatus
                    }
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                           activate()
                        }
                        else {
                            Notify.alert('File Format Not Supported!', {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        activate()
                    });

                    $modalInstance.close('closed');

                }
            }
        }
    }
})();
