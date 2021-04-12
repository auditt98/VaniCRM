<template>
    <div class="background-main">

        <div class="row">
            <div class="col">

            </div>
            <div class="col">
                <TableInDetail :header-columns="taskColumns" :title="'Tasks'"
                >
                    <template slot="body">

                        <tbody>
                        <tr><td colspan="9" class="text-center">Không có dữ liệu</td></tr>
                        </tbody>
                    </template>
                    <template slot="pagination">
                        <div class="table-in-detail-bottom w-100 row justify-content-end m-0" >
                            <div class="form-inline col-sm-4">
                                <label for="exampleFormControlSelect1">Rows per page:</label>
                                <select class="form-control py-0 ml-3" id="exampleFormControlSelect1" >
                                    <option :value="5">5</option>
                                    <option :value="10">10</option>
                                    <option :value="20">20</option>
                                </select>
                            </div>
                            <div class="col-sm-4 pt-2 text-right">
                                <span class="col-sm-8">1 - 200 of 500</span>
                                <span class="col-sm-4 justify-content-between d-inline-flex">
                                    <i class="fa fa-angle-left" aria-hidden="true" ></i>
                                    <i class="fa fa-angle-right" aria-hidden="true" ></i>
                                </span>
                            </div>
                        </div>
                    </template>
                </TableInDetail>
            </div>
        </div>
        <div class="px-3">
            <div class="row">
                <div class="col">
                    <AreaChart/>
                </div>
                <div class="col">
                    <PieChart/>
                </div>
            </div>
            <div class="row bar-chart mx-0">
                <BarChart/>
            </div>
        </div>
    </div>
</template>

<script>
    import TableInDetail from "../common/table/TableInDetail";
    import PieChart from "../common/chart/PieChart";
    import BarChart from "../common/chart/BarChart";
    import AreaChart from "../common/chart/AreaChart";
    export default {
        name: "Report",
        data() {
          return {
              taskColumns: ['TITLE', 'TYPE', 'STATUS', 'DUE DATE', 'PRIORITY', 'OWNER', 'ACCOUNT NAME', 'CONTACT NAME']
          }
        },
        components: {AreaChart, BarChart, PieChart, TableInDetail, }
    }
</script>

<style scoped>
/deep/ th {
    font-size: 12px;
}
/deep/ .bar-chart > div {
    width: 100%;
}

</style>