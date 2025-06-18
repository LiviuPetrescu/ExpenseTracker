<template>
  <div>
    <h2 class="text-2xl font-bold mb-2 text-heading dark:text-heading-dark">Expenses</h2>

    <!-- Filters -->
    <FilterDropdown v-model="filters" :categories="categories" @apply="applyFilters" />

    <!-- Alert -->
    <AlertMessage v-if="successMessage" :message="successMessage" type="success" />

    <!-- Date range warning -->
    <p
      v-if="dateRangeWarning"
      class="text-center text-red-600 dark:text-red-400 text-sm font-medium mb-2"
    >
      {{ dateRangeWarning }}
    </p>

    <!-- No expenses message -->
    <p
      v-if="expenses.length === 0 && !dateRangeWarning"
      class="text-center text-heading dark:text-heading-dark text-base italic p-4 border border-dashed border-border dark:border-border-dark rounded"
    >
      There are no expenses in
      <span class="font-semibold">{{ filters.category || 'any' }}</span>
      category for the selected period.
    </p>

    <!-- Expense List -->
    <ul v-else class="max-h-[500px] overflow-y-auto space-y-2">
      <ExpenseItem
        v-for="expense in expenses"
        :key="expense.id"
        :expense="expense"
        @edit="startEdit"
        @delete="promptDelete"
      />
    </ul>

    <!-- Total -->
    <ExpenseSummary :totalAmount="totalAmount" />

    <!-- Pagination -->
    <PaginationControls
      :currentPage="currentPage"
      :totalPages="totalPages"
      :totalCount="totalCount"
      @change-page="handlePageChange"
    />

    <!-- Confirm Dialog -->
    <ConfirmDialog
      :visible="showConfirm"
      message="Are you sure you want to delete this expense?"
      @confirm="confirmDelete"
      @cancel="cancelDelete"
    />
  </div>
</template>

<script setup>
import { ref, onMounted, computed, watch } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

import ExpenseItem from '@/components/expenses/ExpenseItem.vue'
import ExpenseSummary from '@/components/expenses/ExpenseSummary.vue'
import FilterDropdown from '@/components/ui/FilterDropdown.vue'
import PaginationControls from '@/components/expenses/PaginationControls.vue'
import AlertMessage from '@/components/ui/AlertMessage.vue'
import ConfirmDialog from '@/components/ui/ConfirmDialog.vue'

const emit = defineEmits(['edit-requested'])
const router = useRouter()

const showConfirm = ref(false)
const expenseToDelete = ref(null)

const successMessage = ref('')
const totalAmount = ref(0)
const expenses = ref([])
const categories = ref([])
const totalCount = ref(0)

const currentPage = ref(1)
const goToPageInput = ref(1)
const pageSize = 15
const totalPages = computed(() => {
  const count = Number(totalCount.value) || 0
  return Math.ceil(count / pageSize) || 1
})

const filters = ref({
  category: '',
  startDate: '',
  endDate: '',
})

const dateRangeWarning = ref('')

// Watch for invalid date range
watch(
  () => [filters.value.startDate, filters.value.endDate],
  ([start, end]) => {
    if (start && end && new Date(start) > new Date(end)) {
      dateRangeWarning.value = 'Start date cannot be after end date ⛔'
    } else {
      dateRangeWarning.value = ''
    }
  },
)

const fetchExpenses = async () => {
  try {
    const params = {
      pageNumber: currentPage.value,
      pageSize,
    }

    if (filters.value.category) params.category = filters.value.category
    if (filters.value.startDate) params.startDate = filters.value.startDate
    if (filters.value.endDate) params.endDate = filters.value.endDate

    const response = await axios.get('http://localhost:5079/api/expenses', { params })
    expenses.value = response.data.expenses
    totalAmount.value = response.data.totalAmount
    totalCount.value = response.data.totalCount
  } catch (error) {
    console.error('Failed to fetch expenses:', error)
  }
}

const fetchCategories = async () => {
  try {
    const response = await axios.get('http://localhost:5079/api/expenses/categories')
    categories.value = response.data
  } catch (error) {
    console.error('Failed to fetch categories:', error)
  }
}

const applyFilters = () => {
  if (dateRangeWarning.value) return
  currentPage.value = 1
  goToPageInput.value = 1
  fetchExpenses()
}

const handlePageChange = (page) => {
  currentPage.value = page
  goToPageInput.value = page
}

watch(currentPage, () => {
  fetchExpenses()
})

const startEdit = (expense) => {
  router.push({ name: 'new-expense', query: { edit: JSON.stringify(expense) } })
}

const promptDelete = (expense) => {
  expenseToDelete.value = expense
  showConfirm.value = true
}

const confirmDelete = async () => {
  if (!expenseToDelete.value) return

  try {
    await axios.delete(`http://localhost:5079/api/expenses/${expenseToDelete.value.id}`)
    await fetchExpenses()

    successMessage.value = 'Expense deleted successfully! ✅'

    setTimeout(() => {
      successMessage.value = ''
    }, 3000)
  } catch (error) {
    console.error('Failed to delete expense:', error)
    alert('Error deleting expense.')
  } finally {
    expenseToDelete.value = null
    showConfirm.value = false
  }
}

const cancelDelete = () => {
  expenseToDelete.value = null
  showConfirm.value = false
}

onMounted(() => {
  fetchCategories()
  fetchExpenses()
})

defineExpose({
  fetchExpenses,
})
</script>
