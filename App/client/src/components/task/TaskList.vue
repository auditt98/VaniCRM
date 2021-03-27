<template>
  <div class="background-main">
    <Header/>
    <div class="col-sm-9 mx-auto mt-5">
      <TableInList :header-columns="columns">
        <tbody v-if="tasks && tasks.length > 0">
        <tr v-for="(item,index) in tasks" :key="index">
          <th>{{ item.username }}</th>
          <th>{{ item.firstName }}</th>
          <th>{{ item.lastName }}</th>
          <th>{{ item.phone }}</th>
          <th>{{ item.email }}</th>
          <th>{{ item.skype }}</th>
          <th>
            <span class="action">
              <span @click="edit(item.id)" class="mr-1"><img src="images/newspaper-line.png" alt=""></span>
              <span @click="remove(item.id)"><img src="images/delete-bin-2-line.png" alt=""></span>
            </span>
          </th>

        </tr>
        </tbody>
      </TableInList>
    </div>

  </div>
</template>

<script>
import TableInList from "@/components/common/table/TableInList";
import Header from "@/components/common/Header";

export default {
  name: "TaskList",
  components: {TableInList, Header},
  methods: {
    create() {
      alert(123)
    },
    edit(id) {
      this.$router.push({path: '/task-update', query : { id: id}});
    },
    remove(id) {
      alert('delete' + id)
    }
    ,
    loadTasks() {
      this.tasks = [];

    }
  },

  created() {
    this.currentPage = 1;
    this.pageSize = 10;
    this.loadTasks();
    this.btnCreate = '<button type="button" @click="log()">huyhuy</button>';
  },
  data: function () {
    return {
      tasks: Array,
      currentPage: Number,
      pageSize: Number,
      totalPage: Number,
      columns: [
        {text: 'Username', value: 'username'},
        {text: 'First Name', value: 'firstName'},
        {text: 'Last Name', value: 'lastName'},
        {text: 'Phone', value: 'phone'},
        {text: 'Email', value: 'email'},
        {text: 'Skype', value: 'skype'},
        {text: 'Action', value: 'action'}
      ],
      btnCreate: String
    };
  }
}
</script>

<style scoped>

</style>