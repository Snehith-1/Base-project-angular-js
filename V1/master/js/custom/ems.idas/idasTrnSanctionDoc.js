(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnSanctionDoc', idasTrnSanctionDoc);

    idasTrnSanctionDoc.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function idasTrnSanctionDoc($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        $scope.title = 'idasTrnSanctionDoc';
        var sanction_gid = $location.search().sanction_gid;
        $scope.documentation_list = [];
        activate();

        function activate() {
          
            var url = 'api/IdasTrnSanctionDoc/SanctionDtlsView';
            var params = {
                sanction_gid: sanction_gid
            };
           
            SocketService.getparams(url, params).then(function (resp) {
               
                $scope.sanctionrefno = resp.data.sanctionrefno;
                $scope.SanctionDate = resp.data.SanctionDate;
                $scope.SanctionAmount= resp.data.SanctionAmount;
                $scope.FacilityType = resp.data.FacilityType;
               
                $scope.customerName = resp.data.customerName;
                $scope.Customerurn = resp.data.Customerurn;
                $scope.collateral_security = resp.data.collateral_security;
                $scope.zonalHeadName = resp.data.zonalHeadName;
                $scope.businessHeadName = resp.data.businessHeadName;
                $scope.clusterManager = resp.data.clusterManager;
                $scope.creditManager = resp.data.creditManager;
                $scope.relationshipmgmt = resp.data.relationshipmgmt;
                $scope.customercode = resp.data.customercode;
                $scope.verticalCode = resp.data.verticalCode;
                $scope.contactperson = resp.data.contactperson;
                $scope.mobileno = resp.data.mobileno;
                $scope.addressline1 = resp.data.addressline1;
                $scope.addressline2 = resp.data.addressline2;
                $scope.customer_gid = resp.data.customer_gid;
               });
            var url = "api/IdasMstDocList/GetDocumentList";
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url,params).then(function (resp) {

                $scope.documentlist_gid = resp.data.IDASDocument;
            });

            var url = "api/IdasTrnSanctionDoc/GetTaggedDocList";
            var params = {
                sanction_gid:sanction_gid
            };
            SocketService.getparams(url,params).then(function (resp) {
                $scope.taggeddoc_list = resp.data.MdlTaggedDocument;
              
            });
        }

        $scope.checkall = function (selected) {
            angular.forEach($scope.documentlist_gid, function (val) {
                val.checked = selected;
            });
        }
        $scope.addDoc=function()
        {
            var doc_gid;
            var doclistGId = [];
            angular.forEach($scope.documentlist_gid, function (val) {

                if (val.checked == true) {
                    var doclist_gid = val.documentlist_gid;
                    doc_gid = val.documentlist_gid;
                    doclistGId.push(doclist_gid);
                }
               
 });

            var params = {
                documentlist_gid: doclistGId,
                sanction_gid: sanction_gid,
               customer_gid: $scope.customer_gid
            }

            if (doc_gid != undefined) {
                var url = 'api/IdasTrnSanctionDoc/SanctionDocCreate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        unlockUI();
                        activate();
                        Notify.alert('Document Tagged Successfully!', 'success');
                      
                    }
                    else {
                        unlockUI();
                        Notify.alert('Oops something went wrong!')
                    }

                });
            }
            else {
                Notify.alert('Select Atleast One Document!')
            }
        }
        $scope.untag= function (sanctiondocument_gid)
        {
            var url = "api/IdasTrnSanctionDoc/SanctionDocDelete";
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            SocketService.getparams(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    activate();
                    Notify.alert('Document Un-Tagged Successfully!', 'success');

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning');
                }

            });
        }
       

        $scope.gotoback=function()
        {
            $state.go('app.idasTrnSanctionMgmt');
        }
    }
})();
