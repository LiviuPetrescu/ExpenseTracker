<template>
  <div class="min-h-screen flex flex-col bg-background text-text dark:bg-background-dark dark:text-text-dark">
    <div class="max-w-[1280px] mx-auto p-4 w-full">
      <div class="flex items-center space-x-3 mb-6">
        <img src="/expense.png" alt="Logo" class="h-10 w-10" />
        <h1 class="text-2xl sm:text-3xl font-bold text-heading dark:text-heading-dark">
          Expense Tracker
        </h1>
      </div>

      <AddExpenseForm
        @expense-added="handleExpenseAdded"
        :edit-data="editingExpense"
        @edit-finished="clearEdit"
      />

    <div class="flex-grow overflow-hidden">
      <ExpensesList ref="expensesListRef" @edit-requested="startEdit" />
    </div>

    </div>
  </div>
</template>



<script setup>
import { ref } from 'vue'
import AddExpenseForm from './components/AddExpenseForm.vue'
import ExpensesList from './components/ExpensesList.vue'

const expensesListRef = ref(null)
const editingExpense = ref(null)

const handleExpenseAdded = () => {
  if (expensesListRef.value) {
    expensesListRef.value.fetchExpenses()
  }
}

const startEdit = (expense) => {
  editingExpense.value = { ...expense }
}

const clearEdit = () => {
  editingExpense.value = null
}
</script>
