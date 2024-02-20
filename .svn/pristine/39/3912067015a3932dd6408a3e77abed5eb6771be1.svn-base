(function () {
    'use strict';

    angular
        .module('angle')
        .controller('registerCustomersummary', registerCustomersummary);

    registerCustomersummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', 'DownloaddocumentService'];

    function registerCustomersummary($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'registerCustomersummary';

        activate();


        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/customer/customerdetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.customer_data = resp.data.customer_list;
                 // new code start   
                 if ($scope.customer_data== null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.customer_data.length;
                                        if ($scope.customer_data.length < 100) {
                                            $scope.totalDisplayed = $scope.customer_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.customer_data.length;
            });

            // var url = 'api/employee/employee';
            // SocketService.get(url).then(function (resp) {
            //     $scope.employee_list = resp.data.employee_list;
            // });
        }
        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
             // new code start
        if ($scope.customer_data!= null) {
       
                if (pagecount < $scope.customer_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.customer_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.customer_data.length;
                        Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.popupcustomer = function () {

            $state.go('app.registerCustomer');
        }


        $scope.btntag2legal = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/tagtoLegal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.btnconfirm = function () {

                    var params = {
                        customer_gid: customer_gid,
                        customer_name: customername,
                        tag_remarks: $scope.txttag_remarks,
                        currentcustomer_urn: customer_urn
                    }

                    lockUI();
                    var url = "api/customer/TagtoLegal";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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
            }
        }

        $scope.edit = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $state.go('app.registercustomerEdit');
        }

        $scope.updatecustomerURN = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/updateURN.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.UrnUpdate = function () {

                    var params = {
                        customer_gid: customer_gid,
                        newcustomer_urn: $scope.txtnewcustomerURN,
                        currentcustomer_urn: customer_urn
                    }

                    lockUI();
                    var url = "api/customer/GetNewCustomerURN";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
            }
        }

        $scope.btntag2npa = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/tagtoNPA.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }


                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/Customer/TaggedNPAHistoryList';
                SocketService.getparams(url, params).then(function (resp) {
                    //console.log(resp.data.customertag_list, url);
                    $scope.customertagnpa_list = resp.data.customertagnpa_list;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };



                $scope.btnconfirmnpa = function () {

                    var params = {
                        customer_gid: customer_gid,
                        customer_name: customername,
                        tag_remarks: $scope.txttag_remarks,
                        currentcustomer_urn: customer_urn
                    }

                    lockUI();
                    var url = "api/customer/TagtoNPA";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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
            }
        }

        $scope.exportcustomerdata = function () {
            lockUI();
            var url = 'api/customer/ExportCustomer';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();

                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();
                }

            });
        }

        $scope.delete = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/customer/customerDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };


    }
})();
