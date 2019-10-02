angular.module("umbraco").controller("uGraph.controller", function ($scope, $routeParams, $http) {

    var vm = this;

    vm.docTypeId = $routeParams.id;
    vm.enabled = false;
    vm.dashboard = isNaN(vm.docTypeId);
    vm.dashboardTitle = "Welcome to uGraph";
    vm.hasChanges = false;
    vm.docType;
    vm.maxTabs = 0;
    vm.tabs;

    vm.toggleDocType = toggleDocType;
    vm.toggleProperty = toggleProperty;
    vm.changed = setChanged;
    vm.saveDocType = saveDocType;

    vm.clickTab = changeTab;

    if (!isNaN(vm.docTypeId)) {
        $http({
            method: 'GET',
            url: '/umbraco/api/uGraphapi/getDocType?id=' + vm.docTypeId
        }).then(function (response) {
            vm.docType = response.data;
            vm.tabs = vm.docType.tabs;
            vm.maxTabs = vm.tabs.length;
        }, function (a, b, c, d) {
            alert('error');
        });
    }

    function toggleDocType() {
        vm.docType.enabled = !vm.docType.enabled;
        vm.hasChanges = true;
    }

    function toggleProperty(srcEl) {
        var node = vm["docType"]["tabs"][srcEl.$parent.$index]["properties"][srcEl.$index];
        node["enabled"] = !node["enabled"];
        vm.hasChanges = true;
    }

    function changeTab(viewModel, tab) {
        for (var i = 0; i < viewModel.tabs.length; i++) {
            viewModel.tabs[i].active = false;
        }

        tab.active = true;
    }

    function saveDocType() {
        alert('Save DocType');
    }

    function setChanged() {
        alert('changed');
        vm.hasChanges = true;
    }
});