<template>
<div class="background-main">
  <Header/>
  <div class="col-sm-9 mx-auto mt-5">
    <TableInList :header-columns="columns" :data="leads"/>
  </div>

</div>
</template>

<script>
import Header from "@/components/common/Header";
import TableInList from "@/components/common/table/TableInList";
export default {
  name: "LeadList",
  components: {TableInList, Header},
  data: function() {
    return {
      columns: [
        { text: 'Lead Name', value: 'leadName' },
        { text: 'Company Name', value: 'companyName' },
        { text: 'Email', value: 'email' },
        { text: 'Phone', value: 'phone' },
        { text: 'Lead Source', value: 'leadSource' },
        { text: 'Lead Owner', value: 'leadOwner' },
        { text: 'Priority', value: 'priority' }
        ],
      leads: [
        {leadName: 'Cơ hội', companyName: 'Tom Cruise', email: 'Tom Cruise@gmail.com', phone: '0987654321', leadSource: 'Tom Cruise', leadOwner: 'Tom Cruise',
          priority: {isContent: true, content: '<span class="tag tag-red">high</span>'}},
        {leadName: 'Cơ hội', companyName: 'Tom Cruise', email: 'Tom Cruise@gmail.com', phone: '0987654321', leadSource: 'Tom Cruise', leadOwner: 'Tom Cruise', priority: 'high'},
        {leadName: 'Cơ hội', companyName: 'Tom Cruise', email: 'Tom Cruise@gmail.com', phone: '0987654321', leadSource: 'Tom Cruise', leadOwner: 'Tom Cruise', priority: 'high'},
        {leadName: 'Cơ hội', companyName: 'Tom Cruise', email: 'Tom Cruise@gmail.com', phone: '0987654321', leadSource: 'Tom Cruise', leadOwner: 'Tom Cruise', priority: 'high'},
        {leadName: 'Cơ hội', companyName: 'Tom Cruise', email: 'Tom Cruise@gmail.com', phone: '0987654321', leadSource: 'Tom Cruise', leadOwner: 'Tom Cruise', priority: 'high'},
        {leadName: 'Cơ hội', companyName: 'Tom Cruise', email: 'Tom Cruise@gmail.com', phone: '0987654321', leadSource: 'Tom Cruise', leadOwner: 'Tom Cruise', priority: 'high'},
        {leadName: 'Cơ hội', companyName: 'Tom Cruise', email: 'Tom Cruise@gmail.com', phone: '0987654321', leadSource: 'Tom Cruise', leadOwner: 'Tom Cruise', priority: 'high'},
        {leadName: 'Cơ hội', companyName: 'Tom Cruise', email: 'Tom Cruise@gmail.com', phone: '0987654321', leadSource: 'Tom Cruise', leadOwner: 'Tom Cruise', priority: 'high'}
      ]
    };
  }
}
</script>

<style scoped>

</style>