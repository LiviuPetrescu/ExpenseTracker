<template>
  <div>
    <h2 class="text-2xl font-bold mb-2 text-heading dark:text-heading-dark">Expenses</h2>

    <!-- Filters -->
    <div class="mb-2 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-1">
      <!-- Category Filter -->
      <select
        v-model="filters.category"
        class="w-full text-sm border border-border rounded p-2 focus:outline-none focus:ring-2 focus:ring-heading"
      >
        <option value="">All Categories</option>
        <option v-for="cat in categories" :key="cat" :value="cat">{{ cat }}</option>
      </select>

      <!-- Start Date -->
      <FlatPickr
        v-model="filters.startDate"
        placeholder="Start Date"
        class="w-full text-sm border border-border rounded p-2 focus:outline-none focus:ring-2 focus:ring-heading"
      />

      <!-- End Date -->
      <FlatPickr
        v-model="filters.endDate"
        placeholder="End Date"
        class="w-full text-sm border border-border rounded p-2 focus:outline-none focus:ring-2 focus:ring-heading"
      />

      <!-- Apply Button -->
      <button
        @click="applyFilters"
        class="w-full bg-logo-blue text-white px-4 py-2 rounded hover:bg-logo-blue-dark transition focus:outline-none focus:ring-2 focus:ring-logo-blue"
      >
        Apply Filters
      </button>
    </div>

    <!-- Expense List -->
    <ul class="max-h-[500px] overflow-y-auto space-y-2">
      <li
        v-for="expense in expenses"
        :key="expense.id"
        class="relative border border-border dark:border-border-dark p-3 pr-24 rounded bg-background-soft dark:bg-background-soft-dark"
      >
        <div class="text-sm sm:text-base space-y-1 sm:space-y-0 sm:space-x-2 sm:flex sm:flex-wrap">
          <span class="block sm:inline"><em>{{ expense.category }}</em></span>
          <span class="block sm:inline font-semibold text-heading dark:text-heading-dark">{{ expense.title }}</span>
          <span class="block sm:inline">{{ expense.amount }} RON</span>
          <span class="block sm:inline">Date: {{ expense.date }}</span>
          <span v-if="expense.notes" class="block sm:inline">Note: {{ expense.notes }}</span>
        </div>

        <div class="absolute top-2 right-2 flex flex-col sm:flex-row gap-1 sm:gap-1">
          <button
            @click="startEdit(expense)"
            class="bg-logo-blue text-white px-3 py-1 rounded hover:bg-logo-blue-dark transition focus:outline-none focus:ring-2 focus:ring-logo-blue"
          >
            Edit
          </button>
          <button
            @click="deleteExpense(expense.id)"
            class="bg-logo-blue text-white px-3 py-1 rounded hover:bg-logo-blue-dark transition focus:outline-none focus:ring-2 focus:ring-logo-blue"
          >
            Delete
          </button>
        </div>
      </li>
    </ul>

    <!-- Total -->
    <div class="mt-4 text-lg font-semibold text-heading dark:text-heading-dark">
      Total: {{ totalAmount }} RON
    </div>

    <!-- Pagination -->
    <div class="flex flex-wrap justify-center items-center gap-2 mt-4 text-sm">
      <button
        :disabled="currentPage <= 1"
        @click="goToPreviousPage"
        class="px-4 py-2 bg-gray-200 rounded disabled:opacity-50"
      >
        Previous
      </button>

      <span class="font-semibold text-heading dark:text-heading-dark">
        Page {{ currentPage }} of {{ totalPages || 1 }} ({{ totalCount }} items)
      </span>

      <button
        :disabled="currentPage >= totalPages"
        @click="goToNextPage"
        class="px-4 py-2 bg-gray-200 rounded disabled:opacity-50"
      >
        Next
      </button>

      <!-- Go to page input -->
      <div class="flex items-center gap-1">
        <label for="goToPage">Go to page:</label>
        <input
          id="goToPage"
          type="number"
          min="1"
          :max="totalPages"
          v-model.number="goToPageInput"
          @keyup.enter="goToPage"
          class="w-16 px-2 py-1 border rounded"
        />
        <button @click="goToPage" class="px-2 py-1 bg-logo-blue text-white rounded">Go</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed, watch } from 'vue'
import FlatPickr from 'vue-flatpickr-component'
import 'flatpickr/dist/flatpickr.css'
import axios from 'axios'

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
  endDate: ''
})

const fetchExpenses = async () => {
  try {
    const params = {
      pageNumber: currentPage.value,
      pageSize: pageSize,
    }

    if (filters.value.category) params.category = filters.value.category
    if (filters.value.startDate) params.startDate = filters.value.startDate
    if (filters.value.endDate) params.endDate = filters.value.endDate

    console.log(`Fetching page ${currentPage.value} with`, params)

    const response = await axios.get('http://localhost:5079/api/expenses', { params })
    expenses.value = response.data.expenses
    totalAmount.value = response.data.totalAmount
    totalCount.value = response.data.totalCount
  } catch (error) {
    console.error('Failed to fetch expenses:', error)
  }
}

const applyFilters = () => {
  currentPage.value = 1
  goToPageInput.value = 1
  fetchExpenses()
}

const goToNextPage = () => {
  if (currentPage.value < totalPages.value) {
    currentPage.value++
    goToPageInput.value = currentPage.value
  }
}

const goToPreviousPage = () => {
  if (currentPage.value > 1) {
    currentPage.value--
    goToPageInput.value = currentPage.value
  }
}

const goToPage = () => {
  const target = Math.min(Math.max(1, goToPageInput.value), totalPages.value)
  if (target !== currentPage.value) {
    currentPage.value = target
  }
}

watch(currentPage, () => {
  fetchExpenses()
})

const fetchCategories = async () => {
  try {
    const response = await axios.get('http://localhost:5079/api/expenses/categories')
    categories.value = response.data
  } catch (error) {
    console.error('Failed to fetch categories:', error)
  }
}

const emit = defineEmits(['edit-requested'])

const deleteExpense = async (id) => {
  if (!confirm('Are you sure you want to delete this expense?')) return

  try {
    await axios.delete(`http://localhost:5079/api/expenses/${id}`)
    await fetchExpenses()
  } catch (error) {
    console.error('Failed to delete expense:', error)
    alert('Error deleting expense.')
  }
}

const startEdit = (expense) => {
  emit('edit-requested', expense)
}

onMounted(() => {
  fetchCategories()
  fetchExpenses()
})

defineExpose({
  fetchExpenses
})
</script>
