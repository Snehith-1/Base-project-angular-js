(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDisbursementBuyerDtlViewController', MstDisbursementBuyerDtlViewController);

    MstDisbursementBuyerDtlViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function MstDisbursementBuyerDtlViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDisbursementBuyerDtlViewController';

        var buyer_gid = localStorage.getItem('buyer_gid');

        activate();
        function activate() {          

            var params = {
                buyer_gid: buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/BureauScoreView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.bureauscore_list = resp.data.bureauscore_list;
                unlockUI();
            });

            var url = 'api/MstCreditStatusAdd/buyerMobileNoList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.mobileno_list = resp.data.mobileno_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerEmailAddressList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Emailaddress_list = resp.data.email_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerBankList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.bankdetail_list = resp.data.bank_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerAddressList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.address_list = resp.data.buyeraddress_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerGSTList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.buyergst_list = resp.data.buyergst_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerDetailsEdit';

            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtbuyer_code = resp.data.buyer_code;
                $scope.txtbuyer_name = resp.data.buyer_name;
                $scope.txtcoi_date = resp.data.editcoi_date;
                $scope.txtbusinessstart_date = resp.data.editbusinessstart_date;
                $scope.txtyearin_business = resp.data.year_business;
                $scope.txtmonthsin_business = resp.data.month_business;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txtcin_regno = resp.data.cin_no;
                $scope.txtpan = resp.data.pan_no;
                $scope.txtgstn = resp.data.gst_no;
                $scope.txtfirst_name = resp.data.contactperson_firstname;
                $scope.txtmiddle_name = resp.data.contactperson_middlename;
                $scope.txtlast_name = resp.data.contactperson_lastname;
                $scope.txtcap_limit = resp.data.cap_limit;
                $scope.txtoverall_limit = resp.data.overall_limit;
                $scope.txtbuyer_limit = resp.data.buyer_limit;
                $scope.txtguarantor_limit = resp.data.guarantor_limit;
                $scope.txtborrower_limit = resp.data.borrower_limit;
                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.back = function () {
            var customer_urn = $location.search().customer_urn; 
            var application_gid = $location.search().application_gid; 
            $location.url('app/MstRMInitiateDisbursement?customer_urn=' + customer_urn + '&application_gid=' + application_gid);    
        }

        $scope.uploadeddoc_bureauscore = function (bureauscoreadd_GID) {
            var modalInstance = $modal.open({
                templateUrl: '/bureaudocuments.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    bureauscoreadd_GID: bureauscoreadd_GID
                }
                var url = 'api/MstCreditStatusAdd/BureauDocList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.bureaudoc_list = resp.data.upload_list;
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

                });
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
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_bureaudoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

            }

        }

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.close = function () {
            window.close();
        }

    }
})();