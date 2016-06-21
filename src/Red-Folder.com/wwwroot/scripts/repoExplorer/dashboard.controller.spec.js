describe("Dashboard controller", function () {
    var $scope = null;
    var ctrl = null;
    var sampleData = [
                        { tags: ['tag1']},
                        { tags: ['tag1','tag2']},
                        { tags: ['tag3']}
                    ];

    beforeEach(function () {
        bard.appModule('app');
        bard.inject('$controller', '$q', '$rootScope');
    });

    beforeEach(function () {
        //var service = sinon.stub(repoService, 'getAll').returns($q.when(sampleData));
        var rs = {
            getAll: function () {
                return $q.when(sampleData);
            }
        };
        ctrl = $controller('DashboardController', { repoService: rs });
    });

    it("should have a title of dashboard", function () {
        expect(ctrl.title).to.equal('dashboard');
    });

    describe("after activation", function () {

        beforeEach(function () {
            $rootScope.$apply();
        });

        it("options should have three values", function () {
            expect(ctrl.options.length).to.equal(3);
        });

        it("options should contain 'tag1'", function () {
            expect(ctrl.options).to.include('tag1');
        });

        it("options should not contain 'tag4'", function () {
            expect(ctrl.options).to.not.include('tag4');
        });

        it("selected should have three values", function () {
            expect(ctrl.selected.length).to.equal(3);
        });

        it("selected should contain 'tag1'", function () {
            expect(ctrl.selected).to.include('tag1');
        });

        it("selected should not contain 'tag4'", function () {
            expect(ctrl.selected).to.not.include('tag4');
        });

    });
});