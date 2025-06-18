<template>
  <div>
    <h2 class="text-2xl font-bold mb-4 text-heading dark:text-heading-dark">
      {{ editingExpense ? 'Edit Expense' : 'Add New Expense' }}
    </h2>

    <AlertMessage v-if="successMessage" :message="successMessage" type="success" />

    <AddExpenseForm
      :edit-data="editingExpense"
      @edit-finished="clearEdit"
      @expense-added="handleExpenseAdded"
    />
  </div>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router'
import { ref, onMounted } from 'vue'
import AddExpenseForm from '@/components/newExpenses/AddExpenseForm.vue'
import AlertMessage from '@/components/ui/AlertMessage.vue'

const route = useRoute()
const router = useRouter()
const editingExpense = ref(null)
const successMessage = ref('')

onMounted(() => {
  const editQuery = route.query.edit
  if (editQuery) {
    try {
      editingExpense.value = JSON.parse(editQuery)
    } catch (e) {
      console.error('Invalid edit data:', e)
    }
  }
})

const clearEdit = () => {
  editingExpense.value = null
  router.replace({ query: {} })
}

const handleExpenseAdded = () => {
  const isEdit = !!editingExpense.value

  
  successMessage.value = isEdit
    ? 'Expense updated successfully! ✅'
    : 'Expense added successfully! ✅'

  editingExpense.value = null
  router.replace({ query: {} })

  setTimeout(() => {
    successMessage.value = ''
  }, 3000)
}
</script>
