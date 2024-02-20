(function (){
    'use strict';
    angular
        .module('angle')
        .controller('idasTrnReDespatch360View',idasTrnReDespatch360View);
        idasTrnReDespatch360View.$inject=['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];
        function idasTrnReDespatch360View($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies)
        {
            var vm = this;
            vm.title = 'idasTrnReDespatch360View';
            var redespatch_gid;
            var retrievalrequest_gid;
            activate();

            function activate(){

                redespatch_gid=localStorage.getItem("redespatch_gid");
                $scope.received_type="Direct";

                
                var params={
                    redespatch_gid:redespatch_gid
                };

                var url="api/IdasTrnRecordRetrieval/ReDespatchView360";

                SocketService.getparams(url,params).then(function (resp) {
                  
                    $scope.retrievalrequest_gid=resp.data.retrievalrequest_gid;
                    retrievalrequest_gid=$scope.retrievalrequest_gid;
                    $scope.requested_date=resp.data.requested_date;
                    $scope.requestedby_name=resp.data.requestedby_name;
                    $scope.retrieval_type=resp.data.retrieval_type;
                    $scope.approved_date=resp.data.approved_date;
                    $scope.approvalby_name=resp.data.approvalby_name;
                    $scope.req_remarks=resp.data.req_remarks;
                    $scope.requested_for=resp.data.requested_for;
                    $scope.documentretrieved_status=resp.data.documentretrieved_status;
                    $scope.documentretrieved_mode=resp.data.documentretrieved_mode;

                    $scope.redespatch_gid=resp.data.redespatch_gid;
                    $scope.redespatched_date=resp.data.redespatched_date;
                    $scope.redespatchedby_name=resp.data.redespatchedby_name;
                    $scope.contact_person=resp.data.contact_person; 
                    $scope.remarks=resp.data.remarks;

                
                    $scope.uploaddocument=resp.data.MdlIdasUploadDocument;
                    $scope.RetrivedDtls_list = resp.data.MdlTrnRequired;
                        $scope.totalDisplayed = 100;
                        $scope.total=$scope.RetrivedDtls_list.length;
                       
                        var url = 'api/IdasTrnRecordRetrieval/GetDocReceived';
                        var params={
                            retrievalrequest_gid:$scope.retrievalrequest_gid
                        };
                        SocketService.getparams(url,params).then(function (resp) {
                         
                            $scope.documentretrieved_type = resp.data.documentretrieved_mode;
                            $scope.documentretrieved_date=resp.data.documentretrieved_date;
                            $scope.documentreceivedto_gid=resp.data.documentreceivedto_gid;
                            $scope.documentreceivedto_name=resp.data.documentreceivedto_name;
                            $scope.contactperson_name=resp.data.contactperson_name;
                            $scope.mobile_no=resp.data.mobile_no;
                          
                        });
                    });

                   
                       

                   
            }
            
$scope.loadMore = function (pagecount) {
    if (pagecount == undefined) {
        Notify.alert("Enter the Total Summary Count", "warning");
        return;
    }
    lockUI();
    var Number = parseInt(pagecount);
    if ($scope.RetrivedDtls_list != null) {
        if ($scope.totalDisplayed < $scope.RetrivedDtls_list.length) {
            $scope.totalDisplayed += Number;
            unlockUI();
        }
        else {
            unlockUI();
            Notify.alert(" Total Summary has" + $scope.RetrivedDtls_list.length + " Records Only", "warning");
            return;
        }
    }
    unlockUI();
};
         
            $scope.downloadsdocument = function (val1, val2) {

                var phyPath = val1;            
                var relPath = phyPath.split("EMS");
                var relpath1 = relPath[1].replace("\\", "/");
                var hosts = window.location.host;
                var prefix = location.protocol + "//";
                var str = prefix.concat(hosts, relpath1);
                var link = document.createElement("a");
                var name = val2.split(".")
                link.download = val2;
                var uri = str;
                link.href = uri;
                link.click();
            }
            $scope.back=function(){
                $state.go('app.idasTrnRetrievalReqSummary');
            }

           
           
          
       
        }

})();