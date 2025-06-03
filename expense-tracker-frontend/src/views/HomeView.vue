<template>
  <div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
    <!-- Add Expense Form Section -->
    <section>
      <h2 class="text-2xl font-bold mb-4 text-heading dark:text-heading-dark">
        Add Expense
      </h2>
      <AddExpenseForm
        @expense-added="refreshList"
        @edit-finished="refreshList"
      />
    </section>

    <!-- Expenses List Section -->
    <section>
      <ExpensesList ref="expensesListRef" />
    </section>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import AddExpenseForm from '@/components/AddExpenseForm.vue'
import ExpensesList from '@/components/ExpensesList.vue'

const expensesListRef = ref(null)

const refreshList = () => {
  if (expensesListRef.value) {
    expensesListRef.value.fetchExpenses()
  }
}
</script>
