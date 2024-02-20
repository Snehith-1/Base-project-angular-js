(function (){
    'use strict';
    angular
        .module('angle')
        .controller('idasTrnRetrievalReqView',idasTrnRetrievalReqView);
    idasTrnRetrievalReqView.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', 'DownloaddocumentService'];
    function idasTrnRetrievalReqView($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, DownloaddocumentService)
        {
            var vm = this;
            vm.title = 'idasTrnRetrievalReqView';
            var retrievalrequest_gid;
            activate();

            function activate(){

                vm.calenderrec = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
    
                    vm.openrec = true;
                };
               
    
                vm.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };
    
                vm.formats = ['dd-MM-yyyy'];
                vm.format = vm.formats[0];

                retrievalrequest_gid=localStorage.getItem("retrievalrequest_gid");
                $scope.received_type="Direct";

                
                var params={
                    retrievalrequest_gid:retrievalrequest_gid
                };

                var url="api/IdasTrnRecordRetrieval/RetrievalReqView";

                SocketService.getparams(url,params).then(function (resp) {
                  
                    $scope.requested_date=resp.data.requested_date;
                    $scope.requestedby_name=resp.data.requestedby_name;
                    $scope.retrieval_type=resp.data.retrieval_type;
                    $scope.approved_date=resp.data.approved_date;
                    $scope.approvalby_name=resp.data.approvalby_name;
                    $scope.req_remarks=resp.data.req_remarks;
                    $scope.requested_for=resp.data.requested_for;
                    $scope.documentretrieved_status=resp.data.documentretrieved_status;
                    $scope.documentretrieved_mode=resp.data.documentretrieved_mode;
                    
                    // $scope.customer_urn=resp.data.customer_urn;
                    // $scope.customername=resp.data.customername;
                    // $scope.vertical_code=resp.data.vertical_code;
                    // $scope.businesshead_name=resp.data.businesshead_name;
                    // $scope.zonal_name=resp.data.zonal_name;
                    // $scope.cluster_manager_name=resp.data.cluster_manager_name;
                    // $scope.creditmgmt_name=resp.data.creditmgmt_name;
                    // $scope.relationshipmgmt_name=resp.data.relationshipmgmt_name;

                    // $scope.boxref_no=resp.data.boxref_no;
                    // $scope.stampref_no=resp.data.stampref_no;
                    // $scope.cartonbox_date=resp.data.cartonbox_date;
                    // $scope.remarks=resp.data.remarks;

                    $scope.uploaddocument=resp.data.MdlIdasUploadDocument;
                    if($scope.documentretrieved_mode=="Customer"){
                        $scope.RetrivedDtls_list=resp.data.MdlCustomerDtlsList;
                      
                    }
                    if($scope.documentretrieved_mode=="Box"){
                        $scope.RetrivedDtls_list=resp.data.MdlBoxDtlsList;
                        console.log($scope.RetrivedDtls_list);
                    }
                    if($scope.documentretrieved_mode=="File"){
                        $scope.totalDisplayed = 100;
                        var params = {
                           
                            retrievalrequest_gid:retrievalrequest_gid,
                            documentretrieved_mode:$scope.documentretrieved_mode,
                           
                        };
                        var url = 'api/IdasTrnRecordRetrieval/RetrievalRequestDocDtls';
                        SocketService.post(url, params).then(function (resp) {
                            $scope.RetrivedDtls_list = resp.data.MdlTrnRequired;
                            $scope.total=$scope.RetrivedDtls_list.length;
                            
                        });
                    }
                   
                    });

                    var url = 'api/employee/employee';
                    SocketService.get(url).then(function (resp) {
                        $scope.employee_list = resp.data.employee_list;
                      
                    });

                    if($scope.documentretrieved_status!='Pending'){
                        var url = 'api/IdasTrnRecordRetrieval/GetDocReceived';
                        var params={
                            retrievalrequest_gid:retrievalrequest_gid
                        };
                        SocketService.getparams(url,params).then(function (resp) {
                          
                            $scope.documentretrieved_type = resp.data.documentretrieved_mode;
                            $scope.documentretrieved_date=resp.data.documentretrieved_date;
                            $scope.documentreceivedto_gid=resp.data.documentreceivedto_gid;
                            $scope.documentreceivedto_name=resp.data.documentreceivedto_name;
                            $scope.contactperson_name=resp.data.contactperson_name;
                            $scope.mobile_no=resp.data.mobile_no;
                          
                        });

                    }
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
            $scope.customerdetails = function (customer_gid, id) {
                var params = {
                    customer_gid: customer_gid,
                    retrievalrequest_gid:retrievalrequest_gid,
                    documentretrieved_mode:$scope.documentretrieved_mode,
                    box_gid:""
                };
               
                 var url = 'api/IdasTrnRecordRetrieval/RetrievalRequestDocDtls';
             SocketService.post(url, params).then(function (resp) {
                 console.log(resp.data);
                     $scope.RetrivedDtls_list[id][customer_gid] = resp.data.MdlTrnRequired;
                    
             });
            }
            $scope.boxdetails = function (box_gid, id) {
                var params = {
                    customer_gid: "",
                    retrievalrequest_gid:retrievalrequest_gid,
                    documentretrieved_mode:$scope.documentretrieved_mode,
                    box_gid:box_gid
                };
               
                 var url = 'api/IdasTrnRecordRetrieval/RetrievalRequestDocDtls';
                  SocketService.post(url, params).then(function (resp) {
                 console.log(resp.data);
                     $scope.RetrivedDtls_list[id][box_gid] = resp.data.MdlTrnRequired;
                    
             });
            }
            $scope.downloadsdocument = function (val1, val2) {

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

                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            $scope.back=function(){
                $state.go('app.idasTrnRetrievalReqSummary');
            }

            $scope.Docreceived=function(val){
                var params={
                    retrievalrequestdtls_gid:val,
                    received_type: $scope.retrieval_type

                }

                var url='api/IdasTrnRecordRetrieval/DocReceivedDtls';
                lockUI()
                SocketService.post(url,params).then(function (resp) {
                    unlockUI()
                    if (resp.data.status==true){
                        Notify.alert(resp.data.message, 'success')
                        activate();
                    }
                    else{
                        Notify.alert(resp.data.message, 'warning')
                    }
                });

            }
           
           $scope.receivedSubmit = function () {
              
                if($scope.cboemployeegid ==undefined){
                    Notify.alert("Select the Document Received To", 'warning')
                    return;
                }
                if($scope.contact_person ==undefined){
                    Notify.alert("Enter the Vendor Contact Person", 'warning')
                    return;
                }
                if($scope.receivedDate ==undefined){
                    Notify.alert("Pick the Document Retrieved Date", 'warning')
                    return;
                }
                if($scope.mobile_no ==null){
                    Notify.alert("Enter the Valid Mobile No.", 'warning')
                    return;
                }
                var received_to = $('#employee :selected').text();
                var params={
                    retrievalrequest_gid:retrievalrequest_gid,
                    documentretrieved_mode:$scope.received_type,
                    documentretrieved_date:$scope.receivedDate,
                    documentreceivedto_gid: $scope.cboemployeegid.employee_gid,
                    documentreceivedto_name:received_to,
                    contactperson_name:$scope.contact_person,
                    mobile_no:$scope.mobile_no,
                    received_mode:$scope.retrieval_type
                };
               
                var url='api/IdasTrnRecordRetrieval/DocReceived';
                lockUI()
                SocketService.post(url,params).then(function (resp) {
                    unlockUI()
                    console.log(resp.data);
                    if (resp.data.status==true){
                        Notify.alert(resp.data.message, 'success')
                        
                    }
                    else{
                        Notify.alert(resp.data.message, 'warning')
                    }
                    localStorage.setItem('retrievalrequest_gid',retrievalrequest_gid);
                    $state.go('app.idasTrnRetrievalReqView');
                });

           
        }
       
        }

})();