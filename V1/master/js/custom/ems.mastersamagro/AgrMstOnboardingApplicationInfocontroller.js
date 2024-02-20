(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstOnboardingApplicationInfocontroller', AgrMstOnboardingApplicationInfocontroller);

    AgrMstOnboardingApplicationInfocontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstOnboardingApplicationInfocontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstOnboardingApplicationInfocontroller';
        //var onboard_gid = $location.search().onboard_gid;
        //var lsdynamiclimitmanagementback = $location.search().lsdynamiclimitmanagementback;
        //var lspage = $location.search().lspage;
        //var lstab = $location.search().lstab;
        //$scope.appcreditapproval_gid = $location.search().appcreditapproval_gid;
        //var appcreditapproval_gid = $scope.appcreditapproval_gid;
        //$scope.application_gid = $location.search().application_gid;
        //var application_gid = $scope.application_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var onboard_gid = searchObject.onboard_gid;
        var lsdynamiclimitmanagementback = searchObject.lsdynamiclimitmanagementback;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var lspage = searchObject.lspage;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var lstab = searchObject.lstab;
        $scope.appcreditapproval_gid = searchObject.appcreditapproval_gid;
        var appcreditapproval_gid = $scope.appcreditapproval_gid;
        $scope.application2sanction_gid = searchObject.application2sanction_gid;
        var application2sanction_gid = $scope.application2sanction_gid;
        $scope.application_gid = searchObject.application_gid;
        var application_gid = $scope.application_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lsparent = searchObject.lsparent;
        var lsparent = $scope.lsparent;

        activate();

        function activate() {

          
            var params = {
                onboard_gid: onboard_gid
            }
            var url = 'api/AgrMstBuyerOnboard/GetOnboardLimitManagementdtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.customerref_name = resp.data.customerref_name;
                $scope.application_no = resp.data.application_no;
                $scope.lgltag_status = resp.data.lgltag_status;
                $scope.ProductTypeList = resp.data.MdlProductTypeList;
                $scope.ProductSubTypeList = resp.data.MdlProductSubTypeList;
                $scope.ApplicationList = resp.data.MdlApplicationList;
                $scope.ApplicationFacilityList = resp.data.MdlFaclilitydtl;
               // $scope.MdlPmgExpiryDate = resp.data.MdlPmgExpiryDate;
                $scope.ProductSubTypeApplicationList = resp.data.MdlProductSubTypeApplicationList;

                if ($scope.ApplicationList && $scope.ApplicationList.length != 0) {
                    var getnotexpiredapplication = $scope.ApplicationList.filter(function (el) { return el.contract_status!="Expired" }); 
                    var totalPrice = getnotexpiredapplication.reduce(function (accumulator, item) { 
                        return accumulator + parseInt(item.product_overallamount);
                    }, 0);
                    $scope.customeroverall_limit = (parseInt(totalPrice) || 0).toLocaleString('en-IN');;
                }

                angular.forEach($scope.ApplicationFacilityList, function (value, key) {
                    value.ApprovedLimit = (parseInt(value.ApprovedLimit) || 0).toLocaleString('en-IN'); 
                });

                angular.forEach($scope.ApplicationList, function (value, key) {
                    if (value.application_gid != "") {
                        var getapplicationFacilityListArray = $scope.ApplicationFacilityList.filter(function (el) { return el.application_gid === value.application_gid });
                        if (getapplicationFacilityListArray != null) {
                            value.ApplicationFacilityList = getapplicationFacilityListArray; 
                        } 
                    }
                });

                angular.forEach($scope.ProductSubTypeList, function (value, key) {
                    if (value.application_gid != "") {
                        var getapplicationlist  = $scope.ProductSubTypeApplicationList.filter(function (el) { return el.producttype_gid === value.producttype_gid &&  el.productsubtype_gid === value.productsubtype_gid});
                        if(getapplicationlist != null) {
                            var getapplicationListArray = [];
                            for (var i = 0; i < getapplicationlist.length; i++) {
                                 var getapplicationdtl = $scope.ApplicationList.filter(function (el) { return el.application_gid === getapplicationlist[i].application_gid });
                                if(getapplicationdtl!= null){
                                    var checkduplicateapplicationdtl = getapplicationListArray.filter(function (el) { return el.application_gid === getapplicationlist[i].application_gid });
                                   if(checkduplicateapplicationdtl!=null && checkduplicateapplicationdtl.length==0)
                                      getapplicationListArray.push(getapplicationdtl[0]);
                                }
                                  
                            } 
                            if (getapplicationListArray != null) {
                                value.ApplicationListdtl = getapplicationListArray;  
                                var activedata = getapplicationListArray.filter(function (el) { return el.contract_status === "Active" });
                                var totalPrice = activedata.reduce(function (accumulator, item) {
                                    return accumulator + item.product_overallamount;
                                }, 0);
                                value.overalllimit_subamount = (parseInt(totalPrice) || 0).toLocaleString('en-IN');
                                value.overalllimit_subamountvalue = parseInt(totalPrice); 
                            }
                        }  
                    }
                });

                angular.forEach($scope.ProductTypeList, function (value, key) {
                    if (value.producttype_gid != "") {
                        var getsubproductArray = $scope.ProductSubTypeList.filter(function (el) { return el.producttype_gid === value.producttype_gid });
                        if (getsubproductArray != null) {
                            value.SubproductArray = getsubproductArray; 
                            var totalPrice = getsubproductArray.reduce(function (accumulator, item) {
                                return accumulator + item.overalllimit_subamountvalue;
                            }, 0);
                            value.overalllimit_amount = (parseInt(totalPrice) || 0).toLocaleString('en-IN');
                        } 
                    }
                }); 
                
                if(resp.data.message!=''|| resp.data.message!=null){
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
            //if (lsparent == 'RMonboard') {
            //    $scope.productcompareshow = true;
            //}
            //else if (lsdynamiclimitmanagementback == 'AgrMstCustomerOnboardingSummary'){
            //    $scope.productcompareshow = true;
            //}
            //else {
            //    $scope.productcompareshow = false;
            //}
        }

        $scope.BackPageClick = function () {
            if (lsdynamiclimitmanagementback == 'AgrMstCustomerOnboardingSummary' || lsdynamiclimitmanagementback == 'AgrMstOnboardingApprovalCompleted' || lsdynamiclimitmanagementback == 'AgrMstBuyerApprovedSummary' || lsdynamiclimitmanagementback == 'AgrMstCustomerApprovalSummary'){

                $location.url('app/'+ lsdynamiclimitmanagementback); 
            }
            else if(lsdynamiclimitmanagementback =='AgrApplicationCreationView'){

                $location.url('app/AgrApplicationCreationView?application_gid=' + application_gid + '&lstab='+ lstab); 
            }
            else if(lsdynamiclimitmanagementback =='AgrMstCcCommitteeApplicationView'){

                $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab='+ lstab);
            }
            else if(lsdynamiclimitmanagementback =='AgrTrnCcCommitteeApplicationView'){

                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if(lsdynamiclimitmanagementback =='AgrMstCadApplicationView'){

                $location.url('app/AgrMstCadApplicationView?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lsdynamiclimitmanagementback == 'AgrMstCreateContract') {

                $location.url('app/AgrMstCreateContract?application_gid=' + application_gid + '&lspage=' + lspage);
            }

            else if (lsdynamiclimitmanagementback == 'AgrMstContractEdit') {

                $location.url('app/AgrMstContractEdit?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&lspage=' + lspage);
            }
            
            else if (lsdynamiclimitmanagementback == 'AgrTrnStartCreditUnderwriting') {

                $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=' + lspage);
            }

            else if(lsdynamiclimitmanagementback =='AgrTrnStartScheduledMeeting'){

                if(typeof lspage === 'undefined'){

                    $location.url('app/AgrTrnStartScheduledMeeting?application_gid=' + application_gid);
                }
                else{
                    
                    $location.url('app/AgrTrnStartScheduledMeeting?application_gid=' + application_gid + '&lspage=' + lspage);
                }

                // $location.url('app/AgrMstCadApplicationView?application_gid=' + onboard_gid + '&lspage=' + lspage);
            }

        }

        $scope.facilitydetailsview = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/facilitydetailsviewid.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) { 

                var params = {
                    application_gid: application_gid
                }
                var url = 'api/AgrMstBuyerOnboard/GetOnboardLimitFacilitydtl';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.MdlFaclilitydtl = resp.data.MdlFaclilitydtl; 
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                }; 

            }

        }
        $scope.limit_viewClick = function (application2loan_gid, application_gid) {
            $location.url('app/AgrMstByrProposalProductView?hash=' + cmnfunctionService.encryptURL('application2loan_gid=' + application2loan_gid + "&application_gid=" + application_gid + "&onboard_gid=" + onboard_gid + '&lstab=' + lstab + '&lspage=' + lspage + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsparent=' + lsparent));
        }
        $scope.view = function (application2loan_gid) {
            //$location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL("&onboard_gid=" + onboard_gid));
            $location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL('application2loan_gid=' + application2loan_gid + "&application_gid=" + application_gid + "&onboard_gid=" + onboard_gid + '&lstab=' + lstab + '&lspage=' + lspage + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsparent=' + lsparent));

        }
    }
})();
